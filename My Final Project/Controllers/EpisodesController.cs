using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using My_Final_Project.Areas.Identity.Data;
using My_Final_Project.Models;

namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDBContext _context;

        public EpisodesController(IWebHostEnvironment environment, ApplicationDBContext context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Episodes
        [HttpGet]
        public async Task<JsonResult> GetEpisodes()
        {
            if (_context.Episodes == null)
            {
                return new JsonResult(new { message = "Episodes not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var episodes = await _context.Episodes
                .Include(e => e.Movie)
                .Select(e => new
                {
                    episodeId = e.EpisodeId,
                    episodeNumber = e.EpisodeNumber,
                    duration = e.Duration,
                    title = e.Title,
                    shortDescription = e.ShortDescription,
                    releaseDate = e.ReleaseDate,
                    fileUrl = string.IsNullOrEmpty(e.FileUrl) ? "" : baseUrl + e.FileUrl,
                    movie = e.Movie.Title,
                })
                .ToListAsync();
            return new JsonResult(episodes);
        }

        // GET: api/Episodes/5  
        [HttpGet("{id}")]
        public async Task<JsonResult> GetEpisode(int id)
        {
            if (_context.Episodes == null)
            {
                return new JsonResult(new { message = "Episodes not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var episode = await _context.Episodes
                .Include(e => e.Movie)
                .Where(e => e.EpisodeId == id)
                .Select(e => new
                {
                    episodeId = e.EpisodeId,
                    episodeNumber = e.EpisodeNumber,
                    duration = e.Duration,
                    title = e.Title,
                    shortDescription = e.ShortDescription,
                    releaseDate = e.ReleaseDate,
                    fileUrl = string.IsNullOrEmpty(e.FileUrl) ? "" : baseUrl + e.FileUrl,
                    movie = e.Movie.Title,
                })
                .SingleOrDefaultAsync(e => e.episodeId == id);

            if (episode == null)
            {
                return new JsonResult(new { message = "Episode not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(episode);
        }

        // GET: api/Episodes/Download/{episodeId} 
        [HttpGet("Download/{episodeId}")]
        public async Task<IActionResult> DownloadEpisodeFile(int episodeId)
        {
            var episode = await _context.Episodes.FindAsync(episodeId);

            if (episode == null)
            {
                return NotFound("Episode not found.");
            }

            var fileName = Path.GetFileName(episode.FileUrl);
            var filePath = Path.Combine(_environment.WebRootPath, "Episodes", fileName);

            Console.WriteLine($"File path: {filePath}");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File not found at path: {filePath}");
            }

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "video/mp4";
            }

            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.Headers.Add("Content-Length", memoryStream.Length.ToString());

            return File(memoryStream, contentType, Path.GetFileName(filePath));
        }

        // GET: api/ByMovie/Download/{movieId}
        [HttpGet("ByMovie/{movieId}")]
        public async Task<JsonResult> GetEpisodesByMovieId(int movieId)
        {
            if (_context.Episodes == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Episodes' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var episodes = await _context.Episodes
                .Where(e => e.MovieId == movieId)
                .Select(e => new
                {
                    episodeId = e.EpisodeId,
                    episodeNumber = e.EpisodeNumber,
                    duration = e.Duration,
                    title = e.Title,
                    shortDescription = e.ShortDescription,
                    releaseDate = e.ReleaseDate,
                    fileUrl = string.IsNullOrEmpty(e.FileUrl) ? "" : baseUrl + e.FileUrl,
                    movie = e.Movie.Title,
                    DownloadLink = $"{baseUrl}/api/Episodes/DownloadEpisodeFile/{e.EpisodeId}"
                })
                .ToListAsync();

            if (episodes == null || !episodes.Any())
            {
                return new JsonResult(new { message = "No episodes found for the specified movie." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(episodes);
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<JsonResult> PutEpisode(int id, Episode episode)
        {
            if (id != episode.EpisodeId)
            {
                return new JsonResult(new { message = "Invalid episode ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(episode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodeExists(id))
                {
                    return new JsonResult(new { message = "Episode not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { message = "Episode updated successfully." });
        }

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<JsonResult> PostEpisode(Episode episode)
        {
            if (_context.Episodes == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Episodes' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();

            return new JsonResult(CreatedAtAction("GetEpisode", new { id = episode.EpisodeId }, episode));
        }

        // DELETE: api/Episodes/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteEpisode(int id)
        {
            if (_context.Episodes == null)
            {
                return new JsonResult(new { message = "Episodes not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return new JsonResult(new { message = "Episode not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Episode deleted successfully." });
        }

        private bool EpisodeExists(int id)
        {
            return (_context.Episodes?.Any(e => e.EpisodeId == id)).GetValueOrDefault();
        }
    }
}
