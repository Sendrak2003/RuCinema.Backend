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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CommentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Comments 
        [HttpGet]
        public async Task<JsonResult> GetComments()
        {
            if (_context.Comments == null)
            {
                return new JsonResult(new { message = "Comments not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var comments = await _context.Comments
                .Include(c => c.User)
                .Select(c => new
                {
                    c.CommentId,
                    c.ParentCommentId,
                    c.MovieId,
                    c.UserId,
                    c.Likes,
                    c.CommentText,
                    c.PublicationDate,
                    userName = c.User.FirstName + " " + c.User.LastName,
                    userPhoto = string.IsNullOrEmpty(c.User.userPhoto) ? "" : baseUrl + c.User.userPhoto,
                })
                .ToListAsync();
            return new JsonResult(comments);
        }

        // GET: api/Comments/5 
        [HttpGet("{id}")]
        public async Task<JsonResult> GetComment(int id)
        {
            if (_context.Comments == null)
            {
                return new JsonResult(new { message = "Comments not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var comment = await _context.Comments
                .Include(c => c.User)
                .SingleOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
            {
                return new JsonResult(new { message = "Comment not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var result = new
            {
                comment.CommentId,
                comment.ParentCommentId,
                comment.MovieId,
                comment.UserId,
                comment.Likes,
                comment.CommentText,
                comment.PublicationDate,
                userName = comment.User?.FirstName + " " + comment.User?.LastName,
                userPhoto = string.IsNullOrEmpty(comment.User?.userPhoto) ? "" : baseUrl + comment.User?.userPhoto,
            };

            return new JsonResult(result);
        }
        // GET: api/Comments/review/{reviewId}
        [HttpGet("review/{reviewId}")]
        public async Task<JsonResult> GetCommentsByReviewId(int reviewId)
        {
            if (_context.Comments == null)
            {
                return new JsonResult(new { message = "Comments not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var comments = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.ReviewId == reviewId)
                .ToListAsync();

            if (!comments.Any())
            {
                return new JsonResult(new { message = "No comments found for this review." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var commentsWithUser = comments.Select(comment => new
            {
                comment.CommentId,
                comment.ParentCommentId,
                comment.MovieId,
                comment.UserId,
                comment.Likes,
                comment.CommentText,
                comment.PublicationDate,
                userName = comment.User?.FirstName + " " + comment.User?.LastName,
                userPhoto = string.IsNullOrEmpty(comment.User?.userPhoto) ? "" : baseUrl + comment.User?.userPhoto,
            }).ToList();

            return new JsonResult(commentsWithUser);
        }


        // PUT: api/Comments/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPut("{id}")]
        public async Task<JsonResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return new JsonResult(new { message = "Invalid comment ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return new JsonResult(new { message = "Comment not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(comment); // Возвращаем обновленный комментарий 
        }

        // POST: api/Comments 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 
        [HttpPost]
        public async Task<JsonResult> PostComment(Comment comment)
        {
            if (_context.Comments == null)
            {
                return new JsonResult(new { message = "Comments not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return new JsonResult(comment);
        }

        // DELETE: api/Comments/5 
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteComment(int id)
        {
            if (_context.Comments == null)
            {
                return new JsonResult(new { message = "Comments not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return new JsonResult(new { message = "Comment not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return new JsonResult(comment); // Возвращаем удаленный комментарий 
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}

