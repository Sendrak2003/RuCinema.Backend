using System;
using System.Collections.Generic;
using System.IO;
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
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TagsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Tags  
        [HttpGet]
        public async Task<JsonResult> GetTags()
        {
            if (_context.Tags == null)
            {
                return new JsonResult(new { message = "Tags not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var tags = await _context.Tags
                .Include(t => t.Movies)
                .ToListAsync();
 
            var tagsWithMovies = tags.Select(tag => new
            {
                tag.TagId,
                tag.TagName,
                MovieTitles = tag.Movies.Select(movie => movie.Title).ToList()
            }).ToList();

            return new JsonResult(tagsWithMovies);
        }

        // GET: api/Tags/5  
        [HttpGet("{id}")]
        public async Task<JsonResult> GetTag(int id)
        {
            if (_context.Tags == null)
            {
                return new JsonResult(new { message = "Tags not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var tag = await _context.Tags
                .Include(t => t.Movies)
                .SingleOrDefaultAsync(t => t.TagId == id);

            if (tag == null)
            {
                return new JsonResult(new { message = "Tag not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var tagWithMovies = new
            {
                tag.TagId,
                tag.TagName,
                MovieTitles = tag.Movies.Select(movie => movie.Title).ToList()
            };

            return new JsonResult(tagWithMovies);
        }
        // GET: api/Tags/name
        [HttpGet("name/{name}")]
        public async Task<JsonResult> GetTagByName(string name)
        {
            if (_context.Tags == null)
            {
                return new JsonResult(new { message = "Tags not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var tag = await _context.Tags.SingleOrDefaultAsync(t => t.TagName == name);

            if (tag == null)
            {
                return new JsonResult(new { message = "Tag not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(tag);
        }

        // PUT: api/Tags/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPut("{id}")]
        public async Task<JsonResult> PutTag(int id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return new JsonResult(new { message = "Invalid tag ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var existingTag = await _context.Tags.SingleOrDefaultAsync(t =>
             t.TagName == tag.TagName && t.TagId != id);

            if (existingTag != null)
            {
                return new JsonResult(new { message = "A tag with this FullName already exists." })
                { StatusCode = StatusCodes.Status409Conflict };
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return new JsonResult(new { message = "Tag not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(tag);
        }

        // POST: api/Tags 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPost]
        public async Task<JsonResult> PostTag(Tag tag)
        {
            if (_context.Tags == null)
            {
                return new JsonResult(new { message = "Tags not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var existingTag = await _context.Tags.SingleOrDefaultAsync(t =>
            t.TagName == tag.TagName);

            if (existingTag != null)
            {
                return new JsonResult(new { message = "A tag with this FullName already exists." })
                { StatusCode = StatusCodes.Status409Conflict };
            }

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return new JsonResult(tag);
        }

        // DELETE: api/Tags/5 
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteTag(int id)
        {
            if (_context.Tags == null)
            {
                return new JsonResult(new { message = "Tags not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return new JsonResult(new { message = "Tag not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return new JsonResult(tag);
        }

        private bool TagExists(int id)
        {
            return (_context.Tags?.Any(e => e.TagId == id)).GetValueOrDefault();
        }
    }
}
