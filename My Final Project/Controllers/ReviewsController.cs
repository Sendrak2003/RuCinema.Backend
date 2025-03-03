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
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ReviewsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<JsonResult> GetReviews()
        {
            if (_context.Reviews == null)
            {
                return new JsonResult(new { message = "Reviews not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var reviews = await _context.Reviews
                .Include(r => r.User)
                .ToListAsync();

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var reviewsWithUser = reviews.Select(review => new
            {
                review.ReviewId,
                review.MovieId,
                review.UserId,
                review.Likes,
                review.ReviewText,
                review.PublicationDate,
                userName = review.User?.FirstName + " " + review.User?.LastName,
                userPhoto = string.IsNullOrEmpty(review.User?.userPhoto) ? "" : baseUrl + review.User?.userPhoto,
            }).ToList();

            return new JsonResult(reviewsWithUser);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetReview(int id)
        {
            if (_context.Reviews == null)
            {
                return new JsonResult(new { message = "Reviews not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var review = await _context.Reviews
                .Include(r => r.User)
                .SingleOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return new JsonResult(new { message = "Review not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var reviewWithUser = new
            {
                review.ReviewId,
                review.MovieId,
                review.UserId,
                review.Likes,
                review.ReviewText,
                review.PublicationDate,
                userName = review.User?.FirstName + " " + review.User?.LastName,
                userPhoto = string.IsNullOrEmpty(review.User?.userPhoto) ? "" : baseUrl + review.User?.userPhoto,
            };

            return new JsonResult(reviewWithUser);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<JsonResult> GetReviewsByMovieId(int movieId)
        {
            if (_context.Reviews == null)
            {
                return new JsonResult(new { message = "Reviews not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Comments)
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            if (!reviews.Any())
            {
                return new JsonResult(new { message = "No reviews found for this movie." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var reviewsWithUser = reviews.Select(review => new
            {
                review.ReviewId,
                review.MovieId,
                review.UserId,
                review.Likes,
                review.ReviewText,
                review.PublicationDate,
                userName = review.User?.FirstName + " " + review.User?.LastName,
                userPhoto = string.IsNullOrEmpty(review.User?.userPhoto) ? $"{baseUrl}/defaultPhoto.jpg" : baseUrl + review.User.userPhoto,
                CommentsCount = review.Comments.Count
            }).ToList();

            return new JsonResult(reviewsWithUser);
        }



        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<JsonResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return new JsonResult(new { message = "Invalid review ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return new JsonResult(new { message = "Review not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(review);
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<JsonResult> PostReview(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return new JsonResult(review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteReview(int id)
        {
            if (_context.Reviews == null)
            {
                return new JsonResult(new { message = "Reviews not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return new JsonResult(new { message = "Review not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return new JsonResult(review);
        }

        private bool ReviewExists(int id)
        {
            return (_context.Reviews?.Any(e => e.ReviewId == id)).GetValueOrDefault();
        }
    }
}