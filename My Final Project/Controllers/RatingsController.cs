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
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public RatingsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<JsonResult> GetRatings()
        {
            if (_context.Ratings == null)
            {
                return new JsonResult(new { message = "No ratings found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var ratings = await _context.Ratings
                .Include(r => r.Movie)  
                .Include(r => r.User)   
                .Select(r => new Rating
                {
                    RatingId = r.RatingId,
                    RatingCount = r.RatingCount,
                    RatingDate = r.RatingDate,
                    Movie = new Movie
                    {
                        MovieId = r.Movie.MovieId,
                        Title = r.Movie.Title
                    },
                    User = new ApplicationUser
                    {
                        Id = r.User.Id,
                        UserName = r.User.UserName
                    }
                })
                .ToListAsync();

            return new JsonResult(ratings);
        }


        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetRating(int id)
        {
            if (_context.Ratings == null)
            {
                return new JsonResult(new { message = "Rating not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return new JsonResult(new { message = "Rating not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(rating);
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<JsonResult> PutRating(int id, Rating rating)
        {
            if (id != rating.RatingId)
            {
                return new JsonResult(new { message = "Invalid rating ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Rating updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return new JsonResult(new { message = "Rating not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<JsonResult> PostRating(Rating rating)
        {
            if (_context.Ratings == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Ratings'  is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Rating created successfully.", ratingId = rating.RatingId });
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteRating(int id)
        {
            if (_context.Ratings == null)
            {
                return new JsonResult(new { message = "Rating not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return new JsonResult(new { message = "Rating not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Rating deleted successfully." });
        }

        private bool RatingExists(int id)
        {
            return (_context.Ratings?.Any(e => e.RatingId == id)).GetValueOrDefault();
        }
    }
}