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
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ActorsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Actors 
        [HttpGet]
        public async Task<JsonResult> GetActors()
        {
            if (_context.Actors == null)
            {
                return new JsonResult(new { message = "Actors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var actors = await _context.Actors
                .Include(a => a.Country)
                .Include(a => a.RolesActors)
                .Include(a => a.Movies)
                .ToListAsync();

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var result = actors.Select(actor => new
            {
                actor.Country?.CountryName,
                FlagImage = string.IsNullOrEmpty(actor.Country?.FlagImage) ? "" : baseUrl + actor.Country?.FlagImage,
                ActorName = actor.FullName,
                MovieTitles = actor.Movies.Select(m => m.Title).ToList(),
                Roles = actor.RolesActors.Select(ra => new {
                    RoleName = ra.RoleName,
                    ActorPhotoUrl = string.IsNullOrEmpty(ra.ActorPhotoUrl) ? "" : baseUrl + ra.ActorPhotoUrl,
                }).ToList()
            }).ToList();

            return new JsonResult(result);
        }


        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetActor(int id)
        {
            if (_context.Actors == null)
            {
                return new JsonResult(new { message = "Actors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var actor = await _context.Actors
                .Include(a => a.Country)
                .Include(a => a.RolesActors)
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.ActorId == id);

            if (actor == null)
            {
                return new JsonResult(new { message = "Actor not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

            var result = new
            {
                actor.Country?.CountryName,
                FlagImage = string.IsNullOrEmpty(actor.Country?.FlagImage) ? "" : baseUrl + actor.Country?.FlagImage,
                ActorName = actor.FullName,
                MovieTitles = actor.Movies.Select(m => m.Title).ToList(),
                Roles = actor.RolesActors.Select(ra => new {
                    RoleName = ra.RoleName,
                    ActorPhotoUrl = string.IsNullOrEmpty(ra.ActorPhotoUrl) ? "" : baseUrl + ra.ActorPhotoUrl,
                }).ToList()
            };

            return new JsonResult(result);
        }


        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<JsonResult> PutActor(int id, Actor actor)
        {
            if (id != actor.ActorId)
            {
                return new JsonResult(new { message = "Invalid actor ID." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return new JsonResult(new { message = "Actor not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { message = "Actor updated successfully." });
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<JsonResult> PostActor(Actor actor)
        {
            if (_context.Actors == null)
            {
                return new JsonResult(new { message = "Entity set 'ApplicationDBContext.Actors' is null." }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return new JsonResult(CreatedAtAction("GetActor", new { id = actor.ActorId }, actor));
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteActor(int id)
        {
            if (_context.Actors == null)
            {
                return new JsonResult(new { message = "Actors not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return new JsonResult(new { message = "Actor not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Actor deleted successfully." });
        }

        private bool ActorExists(int id)
        {
            return (_context.Actors?.Any(e => e.ActorId == id)).GetValueOrDefault();
        }
    }
}
