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
    public class AwardsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AwardsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Awards 
        [HttpGet]
        public async Task<JsonResult> GetAwards()
        {
            if (_context.Awards == null)
            {
                return new JsonResult(new { message = "Awards not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var awards = await _context.Awards
                .Include(m => m.Movie)
                .ToListAsync();

            if (!awards.Any())
            {
                return new JsonResult(new { message = "Awards for this movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var results = awards.Select(a => new
                {
                    a.AwardId,
                    a.AwardName,
                    a.AwardYear,
                    awardPhotoUrl = string.IsNullOrEmpty(a.AwardPhotoUrl) ? "" : baseUrl + a.AwardPhotoUrl,
                    movie = a.Movie?.Title,
                })
                .ToList();
            return new JsonResult(results);
        }

        // GET: api/Awards/5 
        [HttpGet("{id}")]
        public async Task<JsonResult> GetAward(int id)
        {
            if (_context.Awards == null)
            {
                return new JsonResult(new { message = "Awards not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var award = await _context.Awards
                .Include(m => m.Movie)
                .SingleOrDefaultAsync(a => a.AwardId == id);

            if (award == null)
            {
                return new JsonResult(new { message = "Award not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var result = new
            {
                award.AwardId,
                award.AwardName,
                award.AwardYear,
                awardPhotoUrl = string.IsNullOrEmpty(award.AwardPhotoUrl) ? "" : baseUrl + award.AwardPhotoUrl,
                movie = award.Movie?.Title,
            };

            return new JsonResult(result);
        }

        // GET: api/Awards/movie/5
        [HttpGet("movie/{movieId}")]
        public async Task<JsonResult> GetAwardsByMovieId(int movieId)
        {
            if (_context.Awards == null)
            {
                return new JsonResult(new { message = "Awards not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var awards = await _context.Awards
                .Where(a => a.MovieId == movieId)
                .ToListAsync();

            if (!awards.Any())
            {
                return new JsonResult(new { message = "Awards for this movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(awards);
        }
        // PUT: api/Awards/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPut("{id}")]
        public async Task<JsonResult> PutAward(int id, Award award)
        {
            if (id != award.AwardId)
            {
                return new JsonResult(new { message = "Invalid award ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(award).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AwardExists(id))
                {
                    return new JsonResult(new { message = "Award not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { message = "Award updated successfully." });
        }

        // POST: api/Awards 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPost]
        public async Task<JsonResult> PostAward(Award award)
        {
            if (_context.Awards == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Awards' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();

            return new JsonResult(CreatedAtAction("GetAward", new { id = award.AwardId }, award));
        }

        // DELETE: api/Awards/5 
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteAward(int id)
        {
            if (_context.Awards == null)
            {
                return new JsonResult(new { message = "Awards not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var award = await _context.Awards.FindAsync(id);
            if (award == null)
            {
                return new JsonResult(new { message = "Award not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Award deleted successfully." });
        }

        private bool AwardExists(int id)
        {
            return (_context.Awards?.Any(e => e.AwardId == id)).GetValueOrDefault();
        }
    }
}
