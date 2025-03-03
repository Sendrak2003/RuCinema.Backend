using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;
using My_Final_Project.Models;

namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")] 
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MoviesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Movies/5   
        [HttpGet("{id}")]
        public async Task<JsonResult> GetMovie(int id)
        {
            if (_context.Movies == null)
            {
                return new JsonResult(new { message = "Movies not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var movie = await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Ratings)
                .Include(m => m.ContentType)
                .Include(m => m.Country)
                .Include(m => m.Directors)
                .Include(m => m.Tags)
                .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return new JsonResult(new { message = "Movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var detailsMovie = new
            {
                Id = movie.MovieId,
                PosterUrl = string.IsNullOrEmpty(movie.CoverImageUrl) ? "" : baseUrl + movie.CoverImageUrl,
                Title = movie.Title,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                Description = movie.ShortDescription,
                Price = movie.Price,
                Currency = movie.Currency,
                ReleasedEpisodes = movie.ReleasedEpisodes,
                TotalEpisodes = movie.TotalEpisodes,
                isFinished = movie.IsFinished,
                ContentType = movie.ContentType.ContentTypeName,
                Country = movie.Country.CountryName,
                Directors = movie.Directors.Select(d => d.FullName).ToList(),
                Genre = movie.Genres.Select(g => g.GenreName).ToList(),
                AverageRating = movie.Ratings.Average(r => (double?)r.RatingCount) ?? 0,
                Tags = movie.Tags.Select(t => t.TagName).ToList()
            };

            return new JsonResult(detailsMovie);
        }
        // GET: api/Movies
        [HttpGet]
        public async Task<JsonResult> GetMovies()
        {
            if (_context.Movies == null)
            {
                return new JsonResult(new { message = "Movies not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var movies = await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Ratings)
                .Include(m => m.ContentType)
                .Include(m => m.Country)
                .Include(m => m.Directors)
                .Include(m => m.Tags)
                .ToListAsync();

            if (movies.Count == 0)
            {
                return new JsonResult(new { message = "No movies found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var detailsMovies = movies.Select(movie => new
            {
                Id = movie.MovieId,
                PosterUrl = string.IsNullOrEmpty(movie.CoverImageUrl) ? "" : $"{baseUrl}{movie.CoverImageUrl}",
                Title = movie.Title,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                Description = movie.ShortDescription,
                Price = movie.Price,
                Currency = movie.Currency,
                ReleasedEpisodes = movie.ReleasedEpisodes,
                TotalEpisodes = movie.TotalEpisodes,
                isFinished = movie.IsFinished,
                ContentType = movie.ContentType.ContentTypeName,
                Country = movie.Country.CountryName,
                Directors = movie.Directors.Select(d => d.FullName).ToList(),
                Genre = movie.Genres.Select(g => g.GenreName).ToList(),
                AverageRating = movie.Ratings.Average(r => (double?)r.RatingCount) ?? 0,
                Tags = movie.Tags.Select(t => t.TagName).ToList()
            });

            return new JsonResult(detailsMovies);
        }


        // GET: api/Movies/Title/movie-title
        [HttpGet("Title/{title}")]
        public async Task<JsonResult> GetMovieByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return new JsonResult(new { message = "Movie title is required." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Title.ToLower().Contains(title.ToLower()));

            if (movie == null)
            {
                return new JsonResult(new { message = "Movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(movie);
        }

        // GET: api/Movies/ShortInfoMovie/contentTypeName
        [HttpGet("ShortInfoMovie/{contentTypeName}")]
        public async Task<JsonResult> GetShortInfoMovies(string contentTypeName)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var movies = await _context.Movies
                .Where(m => m.ContentType.ContentTypeName == contentTypeName)
                .Select(m => new
                {
                    Id = m.MovieId,
                    PosterUrl = string.IsNullOrEmpty(m.CoverImageUrl) ? "" : baseUrl + m.CoverImageUrl,
                    Title = m.Title,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate, 
                    Genre = m.Genres.Select(g => g.GenreName).ToList(),
                    AverageRating = m.Ratings.Average(r => (double?)r.RatingCount) ?? 0
                })
                .ToListAsync();

            var sortedMovies = movies.OrderByDescending(m => m.AverageRating).ToList();

            if (sortedMovies == null || sortedMovies.Count == 0)
            {
                return new JsonResult(new { message = "No movies found for this content type." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(sortedMovies);
        }

        // GET: api/Movies/Search/title/movie-title
        [HttpGet("Search/Title/{title}")]
        public async Task<JsonResult> SearchMovieByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return new JsonResult(new { message = "Movie title is required." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var movies = await _context.Movies
                .Where(m => m.Title.ToLower().Contains(title.ToLower()))
                .Select(m => new
                {
                    Id = m.MovieId,
                    PosterUrl = string.IsNullOrEmpty(m.CoverImageUrl) ? "" : baseUrl + m.CoverImageUrl,
                    Title = m.Title,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Genre = m.Genres.Select(g => g.GenreName).ToList(),
                    AverageRating = m.Ratings.Average(r => (double?)r.RatingCount) ?? 0
                })
                .ToListAsync();

            if (movies == null || movies.Count == 0)
            {
                return new JsonResult(new { message = "No movies found for this title." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(movies);
        }

        // GET: api/Movies/Search/description/movie-description
        [HttpGet("Search/Description/{description}")]
        public async Task<JsonResult> SearchMovieByDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return new JsonResult(new { message = "Movie description is required." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var movies = await _context.Movies
                .Where(m => m.ShortDescription.ToLower().Contains(description.ToLower()))
                .Select(m => new
                {
                    Id = m.MovieId,
                    PosterUrl = string.IsNullOrEmpty(m.CoverImageUrl) ? "" : baseUrl + m.CoverImageUrl,
                    Title = m.Title,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Genre = m.Genres.Select(g => g.GenreName).ToList(),
                    AverageRating = m.Ratings.Average(r => (double?)r.RatingCount) ?? 0
                })
                .ToListAsync();

            if (movies == null || movies.Count == 0)
            {
                return new JsonResult(new { message = "No movies found for this description." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(movies);
        }



        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<JsonResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return new JsonResult(new { message = "ID mismatch." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (movie.Title != null)
            {
                if (MovieExistsByTitle(movie.Title))
                {
                    return new JsonResult(new { message = "Movie with this title already exists." })
                    { StatusCode = StatusCodes.Status400BadRequest };
                }
            }
            else
            {
                return new JsonResult(new { message = "Movie not found." }) 
                { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Movie updated successfully." }) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return new JsonResult(new { message = "Movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<JsonResult> PostMovie(Movie movie)
        {

            if (movie.Title != null)
            {
                if (MovieExistsByTitle(movie.Title))
                {
                    return new JsonResult(new { message = "Movie with this title already exists." })
                    { StatusCode = StatusCodes.Status400BadRequest };
                }
            }
            else
            {
                return new JsonResult(new { message = "Movie not found." })
                { StatusCode = StatusCodes.Status404NotFound };
            }


            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return new JsonResult(movie) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return new JsonResult(new { message = "Movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Movie deleted successfully." }) { StatusCode = StatusCodes.Status204NoContent };
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        private bool MovieExistsByTitle(string title)
        {
            return _context.Movies.Any(m => m.Title.ToLower().Contains(title.ToLower()));
        }
    }
}