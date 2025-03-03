using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace My_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/User
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return new JsonResult(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult("User not found");
            }

            return new JsonResult(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<JsonResult> UpdateUser(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult("ID mismatch");
            }

            var existingUser = await _userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult("User not found");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.CountryId = user.CountryId;

            var result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
                return new JsonResult("User updated successfully");
            }

            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new JsonResult(result.Errors);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult("User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;
                return new JsonResult("User deleted successfully");
            }

            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new JsonResult(result.Errors);
        }
    }
}
