using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;
using My_Final_Project.Models;


namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesActorsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public RolesActorsController(ApplicationDBContext context)
        {
            _context = context;
        }
        //GET: api/RolesActors
        [HttpGet]
        public async Task<JsonResult> GetRolesActors()
        {
            if (_context.RolesActors == null)
            {
                return new JsonResult(new { message = "Roles Actors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var rolesActors = await _context.RolesActors
                .Include(ra => ra.Actor)
                .Include(ra => ra.Movie)
                .ToListAsync();

            if (rolesActors == null)
            {
                return new JsonResult(new { message = "Roles Actor not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var result = rolesActors.Select(ra => new
            {
                ra.RoleId,
                ra.RoleName,
                ra.ActorId,
                ra.MovieId,
                ActorPhotoUrl = string.IsNullOrEmpty(ra.ActorPhotoUrl) ? "" : baseUrl + ra.ActorPhotoUrl,
                ActorName = ra.Actor?.FullName,
                MovieTitle = ra.Movie?.Title
            }).ToList();

            return new JsonResult(result);
        }

        //GET: api/RolesActors/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetRolesActor(int id)
        {
            if (_context.RolesActors == null)
            {
                return new JsonResult(new { message = "Roles Actor not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var rolesActor = await _context.RolesActors
                .Include(ra => ra.Actor)
                .Include(ra => ra.Movie)
                .SingleOrDefaultAsync(ra => ra.RoleId == id);

            if (rolesActor == null)
            {
                return new JsonResult(new { message = "Roles Actor not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";


            var result = new
            {
                rolesActor.RoleId,
                rolesActor.RoleName,
                rolesActor.ActorId,
                rolesActor.MovieId,
                ActorPhotoUrl = string.IsNullOrEmpty(rolesActor.ActorPhotoUrl) ? "" : baseUrl + rolesActor.ActorPhotoUrl,
                ActorName = rolesActor.Actor?.FullName,
                MovieTitle = rolesActor.Movie?.Title
            };

            return new JsonResult(result);
        }

        //GET: api/RolesActors/movie/5 
        [HttpGet("movie/{movieId}")]
        public async Task<JsonResult> GetRolesByMovieId(int movieId)
        {
            if (_context.RolesActors == null)
            {
                return new JsonResult(new { message = "Roles Actors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var rolesActors = await _context.RolesActors
                .Include(ra => ra.Actor)
                .Where(ra => ra.MovieId == movieId)
                .ToListAsync();

            if (!rolesActors.Any())
            {
                return new JsonResult(new { message = "Roles for this movie not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var results = rolesActors.Select(ra => new
            {
                ra.RoleId,
                ra.RoleName,
                ra.ActorId,
                ra.MovieId,
                ActorPhotoUrl = string.IsNullOrEmpty(ra.ActorPhotoUrl) ? "" : baseUrl + ra.ActorPhotoUrl,
                ActorName = ra.Actor?.FullName,
                MovieTitle = ra.Movie?.Title
            }).ToList();

            return new JsonResult(results);
        }


        // PUT: api/RolesActors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<JsonResult> PutRolesActor(int id, RolesActor rolesActor)
        {
            if (id != rolesActor.RoleId)
            {
                return new JsonResult(new { message = "Invalid RolesActor ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(rolesActor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesActorExists(id))
                {
                    return new JsonResult(new { message = "RolesActor not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { message = "RolesActor updated successfully." });
        }

        // POST: api/RolesActors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<JsonResult> PostRolesActor(RolesActor rolesActor)
        {
            if (_context.RolesActors == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.RolesActors' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            _context.RolesActors.Add(rolesActor);
            await _context.SaveChangesAsync();

            return new JsonResult(CreatedAtAction("GetRolesActor", new { id = rolesActor.RoleId }, rolesActor));
        }

        // DELETE: api/RolesActors/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteRolesActor(int id)
        {
            if (_context.RolesActors == null)
            {
                return new JsonResult(new { message = "RolesActors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var rolesActor = await _context.RolesActors.FindAsync(id);
            if (rolesActor == null)
            {
                return new JsonResult(new { message = "RolesActor not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.RolesActors.Remove(rolesActor);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "RolesActor deleted successfully." });
        }

        private bool RolesActorExists(int id)
        {
            return (_context.RolesActors?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}

