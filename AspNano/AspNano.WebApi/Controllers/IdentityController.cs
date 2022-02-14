using AspNano.Application.Services.VenueService;
using AspNano.Common.HelperClasses;
using AspNano.DTOs.VenueDTOs;
using AspNano.Enums;
using AspNano.Infrastructure.Persistence;
using AspNano.WebApi.Models;
using AspNano.WebApi.Validators;
using AspNano.WebApi.Validators.Venues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AspNano.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public  class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        // While Uploading to a server we will activate the smtp
        //not high priority but need a reset password facility -- available to all users
        //[HttpPost("reset-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        //{
        //    return Ok(await _identityService.ResetPasswordAsync(request));
        //}
        //View all users in a list 
        [Route("UserList")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //This will be used for admin users to view a listing of all their users

            var usersList = await _userManager.Users.ToListAsync();
            return Ok(usersList);
        }
        //BASIC PERMISSIONS -----------------------------
        [HttpGet("profile")] //get your own profile (basic user permissions)
        public async Task<IActionResult> GetProfileDetailsAsync()
        {
            var usersList=await _userManager.Users.Where(x=>x.Id== TenantUserInfo.UserID).FirstOrDefaultAsync();
            return Ok(usersList);
        }

        [HttpGet("profile/{id}")] //ADMIN Get a profile (clicking edit in the user list)
        public async Task<IActionResult> GetProfileDetailsAsync(Guid id)
        {
            var usersList = await _userManager.Users.Where(x => x.Id == Convert.ToString(id)).FirstOrDefaultAsync();
            return Ok(usersList);
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] MyLoginModelType myLoginModel)
        {

            var userExist = await _userManager.FindByEmailAsync(myLoginModel.Email);
            if (userExist != null)
            {
                return Ok(new { Result = "User Already Exist" });
            }

            else
            {
                var user = new ApplicationUser
                {
                    UserName = myLoginModel.Email,
                    Email = myLoginModel.Email,
                    EmailConfirmed = false,
                    TenantId= Guid.Parse(TenantUserInfo.TenantID)
                };

                var result = await _userManager.CreateAsync(user, myLoginModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    return Ok(new { Result = "User Created Successfully" });
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        stringBuilder.Append(error.Description);
                    }
                    return Ok(new { Result = $"Refister Fail:{stringBuilder.ToString()}" });
                }
            }
        }
        [HttpPut("profile")] //Update your own profile (basic user permissions)
        public async Task<IActionResult> UpdateProfileAsync(UpdateIdentityUserViewModel request)
        {
            
            var user = await _userManager.FindByIdAsync(TenantUserInfo.UserID);

            if (user == null)
            {
                return NotFound();
            }
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPut("profile/{id}")] //ADMIN Update A user
        public async Task<IActionResult> UpdateUserAsync(UpdateIdentityUserViewModel request, Guid id)
        {
            if (id == Guid.Parse(TenantUserInfo.UserID))
            {
                return BadRequest(new { Result = "You cannot update the logged-in user." });
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            await _userManager.UpdateAsync(user);
            return Ok();
        }
    }

}