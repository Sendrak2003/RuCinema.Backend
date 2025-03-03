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
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public GenresController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<JsonResult> GetGenres()
        {
            if (_context.Genres == null)
            {
                return new JsonResult(new { message = "No genres found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var genres = await _context.Genres
                .Include(g => g.Movies) 
                .Select(g => new
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName,
                    Movies = g.Movies.Select(m => new {
                        MovieId = m.MovieId,
                        Title = m.Title
                    })
                })
                .ToListAsync();

            return new JsonResult(genres);
        }


        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetGenre(int id)
        {
            if (_context.Genres == null)
            {
                return new JsonResult(new { message = "No genres found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return new JsonResult(new { message = "Genre not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(genre);
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<JsonResult> PutGenre(int id, Genre genre) 
        {
            if (id != genre.GenreId)
            {
                return new JsonResult(new { message = "Invalid genre ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(genre) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return new JsonResult(new { message = "Genre not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    return new JsonResult(new { message = "Error updating genre." }) { StatusCode = StatusCodes.Status500InternalServerError };
                }
            }
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<JsonResult> PostGenre(Genre genre)
        {
            if (_context.Genres == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Genres' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return new JsonResult(genre) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteGenre(int id)
        {
            if (_context.Genres == null)
            {
                return new JsonResult(new { message = "No genres found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return new JsonResult(new { message = "Genre not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Genre deleted successfully." }) { StatusCode = StatusCodes.Status204NoContent };
        }

        [HttpGet("ExistsByName/{name}")]
        public async Task<JsonResult> GenreExistsByName(string name)
        {
            if (_context.Genres == null)
            {
                return new JsonResult(new { message = "No genres found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var exists = await _context.Genres.AnyAsync(g => g.GenreName == name);
            return new JsonResult(new { exists = exists });
        }

        
        [HttpGet("FindByName/{name}")]
        public async Task<JsonResult> FindGenreByName(string name)
        {
            if (_context.Genres == null)
            {
                return new JsonResult(new { message = "No genres found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == name);
            if (genre == null)
            {
                return new JsonResult(new { message = "Genre not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(genre);
        }

        private bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.GenreId == id)).GetValueOrDefault();
        }
    }
}





