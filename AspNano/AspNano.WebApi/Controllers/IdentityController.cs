//Uncomment code to view
//This was taken from my modified FSH implementation







//using ServerApp.Application.Abstractions.Services.Identity;
//using ServerApp.Domain.Constants;
//using ServerApp.Infrastructure.Identity.Permissions;
//using ServerApp.Shared.DTOs.Identity;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ServerApp.Host.Controllers.Identity;

//[ApiController]
//[Route("api/[controller]")]
//public sealed class IdentityController : ControllerBase
//{
//    private readonly ICurrentUser _user;
//    private readonly IIdentityService _identityService;
//    private readonly IUserService _userService;

//    public IdentityController(IIdentityService identityService, ICurrentUser user, IUserService userService)
//    {
//        _identityService = identityService;
//        _user = user;
//        _userService = userService;
//    }


//    //BASIC PERMISSIONS -----------------------------
//    [HttpGet("profile")] //get your own profile (basic user permissions)
//    public async Task<IActionResult> GetProfileDetailsAsync()
//    {
//        return Ok(await _userService.GetAsync(_user.GetUserId().ToString()));
//    }

//    [HttpPut("profile")] //Update your own profile (basic user permissions)
//    public async Task<IActionResult> UpdateProfileAsync(UpdateProfileRequest request)
//    {
//        return Ok(await _identityService.UpdateProfileAsync(request, _user.GetUserId().ToString()));
//    }



//    //ADMIN PERMISSIONS -------------------
//    //For admin users to create new users
//    [HttpPost("register")]
    
//    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
//    {
//        string origin = GenerateOrigin();
//        return Ok(await _identityService.RegisterAsync(request, origin));
//    }




//    //View all users in a list 
//    [Route("UserList")]
//    [HttpGet]
//    public async Task<IActionResult> GetAllAsync()
//    {
//        //This will be used for admin users to view a listing of all their users

//        var users = await _userService.GetAllAsync();
//        return Ok(users);
//    }



//    [HttpGet("profile/{id}")] //ADMIN Get a profile (clicking edit in the user list)
//    public async Task<IActionResult> GetProfileDetailsAsync(Guid id)
//    {
//        return Ok(await _userService.GetAsync(id.ToString()));
//    }

//    [HttpPut("profile/{id}")] //ADMIN Update A user
//    public async Task<IActionResult> UpdateUserAsync(UpdateProfileRequest request, Guid id)
//    {
//        return Ok(await _identityService.UpdateProfileAsync(request, id.ToString()));
//    }




//    //not high priority but need a reset password facility -- available to all users
//    [HttpPost("reset-password")]
//    [AllowAnonymous]
//    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
//    {
//        return Ok(await _identityService.ResetPasswordAsync(request));
//    }

//}