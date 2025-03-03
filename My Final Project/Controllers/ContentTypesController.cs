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
    public class ContentTypesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ContentTypesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ContentTypes 
        [HttpGet]
        public async Task<JsonResult> GetContentTypes()
        {
            var contentTypes = await _context.ContentTypes
                .Include(ct => ct.Movies)
                .Select(ct => new ContentType
                {
                    ContentTypeId = ct.ContentTypeId,
                    ContentTypeName = ct.ContentTypeName,
                    Movies = ct.Movies
                })
                .ToListAsync();

            if (contentTypes == null || contentTypes.Count == 0)
            {
                return new JsonResult(new { message = "No content type found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(contentTypes);
        }

        // GET: api/ContentTypes/5 
        [HttpGet("{id}")]
        public async Task<JsonResult> GetContentType(int id)
        {
            var contentType = await _context.ContentTypes.FindAsync(id);

            if (contentType == null)
            {
                return new JsonResult(new { message = "Content type not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(contentType);
        }

        // PUT: api/ContentTypes/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPut("{id}")]
        public async Task<JsonResult> PutContentType(int id, ContentType contentType)
        {
            if (id != contentType.ContentTypeId)
            {
                return new JsonResult(new { message = "Invalid content type ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(contentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Content type updated successfully." }) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentTypeExists(id))
                {
                    return new JsonResult(new { message = "Content type not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/ContentTypes 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPost]
        public async Task<JsonResult> PostContentType(ContentType contentType)
        {
            _context.ContentTypes.Add(contentType);
            await _context.SaveChangesAsync();

            return new JsonResult(contentType) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE: api/ContentTypes/5 
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteContentType(int id)
        {
            var contentType = await _context.ContentTypes.FindAsync(id);
            if (contentType == null)
            {
                return new JsonResult(new { message = "Content type not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.ContentTypes.Remove(contentType);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Content type deleted successfully." }) { StatusCode = StatusCodes.Status204NoContent };
        }

        // GET: api/ContentTypes/search?name=someName 
        [HttpGet("search")]
        public async Task<JsonResult> SearchContentTypesByName(string name)
        {
            var contentTypes = await _context.ContentTypes.Where(ct => ct.ContentTypeName.Contains(name)).ToListAsync();

            if (contentTypes == null || contentTypes.Count == 0)
            {
                return new JsonResult(new { message = "Content type not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(contentTypes);
        }

        // POST: api/ContentTypes/exists?name=someName
        [HttpPost("exists")]
        public async Task<JsonResult> ContentTypeExistsByName(string name)
        {
            var contentType = await _context.ContentTypes.FirstOrDefaultAsync(ct => ct.ContentTypeName == name);

            if (contentType == null)
            {
                return new JsonResult(new { exists = false }) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(new { exists = true }) { StatusCode = StatusCodes.Status200OK };
        }

        private bool ContentTypeExists(int id)
        {
            return (_context.ContentTypes?.Any(e => e.ContentTypeId == id)).GetValueOrDefault();
        }
    }
}
