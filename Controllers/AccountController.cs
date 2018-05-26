using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Spice.Models;
using Spice.ViewModels;

namespace Spice.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private IConfiguration _config;
        private SpiceDBContext _context;
        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(IConfiguration config, SpiceDBContext context, UserManager<User> userManager, SignInManager<User> signManager, RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
        }

        //..........................................
        [HttpPost("/token")]
        public async Task Token([FromBody] LoginVM model)
        {

            var email = model.Email;
            var password = model.Password;

            var identity = await GetIdentityAsync(email, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    notBefore: now,
                    claims: identity.Claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string email, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);
            bool pw = await _userManager.CheckPasswordAsync(user, password);
            if (pw)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                };
                claims.Add(new Claim("username", user.UserName));
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (string roleName in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }
                foreach (string roleName in userRoles)
                {
                    claims.Add(new Claim("role", roleName));
                }
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [HttpPost("/registration")]
        public async Task Register([FromBody] RegisterVM model)
        {

            var user = new User() { UserName = model.Email, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var identity = await GetIdentityAsync(model.Email, model.Password);
                if (identity == null)
                {
                    Response.StatusCode = 400;
                    await Response.WriteAsync("Invalid username or password.");
                    return;
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                        issuer: _config["Jwt:Issuer"],
                        audience: _config["Jwt:Issuer"],
                        notBefore: now,
                        claims: identity.Claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                User userr = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.AddToRoleAsync(userr, "expert");

                var response = new
                {
                    access_token = encodedJwt
                };

                // сериализация ответа
                Response.ContentType = "application/json";
                await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                return;
            }

            await Response.WriteAsync(JsonConvert.SerializeObject(new { result = "error"}, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            return;

        }


    }
}