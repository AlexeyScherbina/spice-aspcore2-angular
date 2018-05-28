using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Spice.Models;

namespace Spice.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {

        private SpiceDBContext _context;


        public CategoryController(SpiceDBContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task GetHierarchy()
        {
            var cats = _context.Category
                .Include(proc => proc.Process)
                .ThenInclude(prac => prac.Practice)
                .ToList();
            var response = cats;

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}