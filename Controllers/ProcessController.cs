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
    public class ProcessController : Controller
    {

        private SpiceDBContext _context;


        public ProcessController(SpiceDBContext context)
        {
            _context = context;
        }


        [HttpPost("[action]")]
        public async Task Add([FromBody] Process model)
        {

            Process c = new Process()
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = (int)model.CategoryId
            };

            var dd = _context.Process.Add(c);
            var ddd = await _context.SaveChangesAsync();
            var response = "success";

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [HttpPost("[action]")]
        public async Task Update([FromBody] Process model)
        {
            var catToUpdate = await _context.Process.SingleOrDefaultAsync(c => c.ProcessId == model.ProcessId);

            catToUpdate.Title = model.Title;
            catToUpdate.Description = model.Description;

            //_context.Entry(catToUpdate).State = EntityState.Modified;


            _context.Update(catToUpdate);
            await _context.SaveChangesAsync();
            var response = "success";
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [HttpPost("[action]")]
        public async Task Delete([FromBody] Process model)
        {
            var catToDelete = await _context.Process.SingleOrDefaultAsync(c => c.ProcessId == model.ProcessId);

            var response = _context.Process.Remove(catToDelete);
            await _context.SaveChangesAsync();
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}