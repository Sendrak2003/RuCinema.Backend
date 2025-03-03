using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;
using My_Final_Project.Models;

namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DirectorsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Directors 
        [HttpGet]
        public async Task<JsonResult> GetDirectors()
        {
            if (_context.Directors == null)
            {
                return new JsonResult(new { message = "Directors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            return new JsonResult(await _context.Directors.ToListAsync());
        }

        // GET: api/Directors/5 
        [HttpGet("{id}")]
        public async Task<JsonResult> GetDirector(int id)
        {
            if (_context.Directors == null)
            {
                return new JsonResult(new { message = "Directors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var director = await _context.Directors.SingleOrDefaultAsync(d=> d.DirectorId == id);

            if (director == null)
            {
                return new JsonResult(new { message = "Director not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(director);
        }

        // GET: api/Directors/FullName
        [HttpGet("FullName/{fullName}")]
        public async Task<JsonResult> GetDirectorByFullName(string fullName)
        {
            if (_context.Directors == null)
            {
                return new JsonResult(new { message = "Directors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var director = await _context.Directors.SingleOrDefaultAsync(d => d.FullName == fullName);

            if (director == null)
            {
                return new JsonResult(new { message = "Director not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(director);
        }

        // PUT: api/Directors/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPut("{id}")]
        public async Task<JsonResult> PutDirector(int id, Director director)
        {
            if (id != director.DirectorId)
            {
                return new JsonResult(new { message = "Invalid director ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var existingDirector = await _context.Directors.SingleOrDefaultAsync(d =>
                d.FullName == director.FullName && d.DirectorId != id);

            if (existingDirector != null)
            {
                return new JsonResult(new { message = "A director with this FullName already exists." })
                { StatusCode = StatusCodes.Status409Conflict };
            }

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return new JsonResult(new { message = "Director not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { message = "Director updated successfully." });
        }

        // POST: api/Directors 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPost]
        public async Task<JsonResult> PostDirector(Director director)
        {
            if (_context.Directors == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Directors' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            var existingDirector = await _context.Directors.SingleOrDefaultAsync(d => d.FullName == director.FullName);

            if (existingDirector != null)
            {
                return new JsonResult(new { message = "A director with this FullName already exists." })
                { StatusCode = StatusCodes.Status409Conflict };
            }

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return new JsonResult(CreatedAtAction("GetDirector", new { id = director.DirectorId }, director));
        }

        // DELETE: api/Directors/5 
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteDirector(int id)
        {
            if (_context.Directors == null)
            {
                return new JsonResult(new { message = "Directors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var director = await _context.Directors.SingleOrDefaultAsync(d => d.DirectorId == id);
            if (director == null)
            {
                return new JsonResult(new { message = "Director not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Director deleted successfully." });
        }

        private bool DirectorExists(int id)
        {
            return (_context.Directors?.Any(e => e.DirectorId == id)).GetValueOrDefault();
        }
    }
}
