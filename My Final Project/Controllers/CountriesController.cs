using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;
using My_Final_Project.Models;

namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")] // Комментарий: разкомментируйте, если требуется авторизация
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CountriesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<JsonResult> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();

            if (countries == null || countries.Count == 0)
            {
                return new JsonResult(new { message = "No countries found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return new JsonResult(new { message = "Country not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(country);
        }

        // GET: api/Countries/Name/country-name
        [HttpGet("Name/{name}")]
        public async Task<JsonResult> GetCountryByName(string name)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.CountryName.ToLower().Contains(name.ToLower()));

            if (country == null)
            {
                return new JsonResult(new { message = "Country not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return new JsonResult(country);
        }

        // PUT: api/Countries/5
        [HttpPut("{id}")]
        public async Task<JsonResult> PutCountry(int id, Country country)
        {
            if (id != country.CountryId)
            {
                return new JsonResult(new { message = "ID mismatch." }) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (country.CountryName != null)
            {
                if (CountryExistsByName(country.CountryName))
                {
                    return new JsonResult(new { message = "Country with this name already exists." })
                    { StatusCode = StatusCodes.Status400BadRequest };
                }
            }
            else
            {
                return new JsonResult(new { message = "Country not found." })
                { StatusCode = StatusCodes.Status400BadRequest };
            }
           
            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new { message = "Country updated successfully." }) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return new JsonResult(new { message = "Country not found." }) { StatusCode = StatusCodes.Status404NotFound };
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<JsonResult> PostCountry(Country country)
        {
            if (country.CountryName != null)
            {
                if (CountryExistsByName(country.CountryName))
                {
                    return new JsonResult(new { message = "Country with this name already exists." })
                    { StatusCode = StatusCodes.Status400BadRequest };
                }
            }
            else
            {
                return new JsonResult(new { message = "Country not found." })
                { StatusCode = StatusCodes.Status400BadRequest };
            }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return new JsonResult(country) { StatusCode = StatusCodes.Status201Created };
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return new JsonResult(new { message = "Country not found." }) { StatusCode = StatusCodes.Status404NotFound };
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return new JsonResult(new { message = "Country deleted successfully." }) { StatusCode = StatusCodes.Status204NoContent };
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }

        private bool CountryExistsByName(string name)
        {
            return _context.Countries.Any(c => c.CountryName.ToLower().Contains(name.ToLower()));
        }
    }
}