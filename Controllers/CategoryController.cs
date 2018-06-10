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

        [HttpPost("[action]")]
        public async Task Add([FromBody] Category model)
        {

            Category c = new Category()
            {
                Title = model.Title,
                Description = model.Description
            };
            
            var response = _context.Category.Add(c);

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [HttpPost("[action]")]
        public async Task Update([FromBody] Category model)
        {
            var catToUpdate = await _context.Category.SingleOrDefaultAsync(c => c.CategoryId == model.CategoryId);

            catToUpdate.Title = model.Title;
            catToUpdate.Description = model.Description;

            //_context.Entry(catToUpdate).State = EntityState.Modified;


            var response = _context.Update(catToUpdate);
            await _context.SaveChangesAsync();
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [HttpPost("[action]")]
        public async Task Delete([FromBody] Category model)
        {
            var catToDelete = await _context.Category.SingleOrDefaultAsync(c => c.CategoryId == model.CategoryId);

            var response = _context.Category.Remove(catToDelete);
            await _context.SaveChangesAsync();
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}