using AspNano.Application.Services.TenantService;
using AspNano.DTOs.AuthDTOs;
using AspNano.DTOs.TenantDTOs;
using AspNano.Enums;
using AspNano.Infrastructure;
using AspNano.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


//Rename this to Tokens controller

//It should only have two endpoints, get token and refresh token
//registration to be done in Identity Controller (by admins only, no open registration)
namespace AspNano.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITenantService _tenantService;

        public TokensController(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ITenantService tenantService
            )
        {
            _dbContext=dbContext;
            _userManager=userManager;   
            _signInManager=signInManager;
            _roleManager=roleManager;
            _configuration=configuration;
            _tenantService=tenantService;
        }


        [AllowAnonymous]
        [HttpPost]
        //api/tokens
        public async Task<IActionResult> Login([FromBody] TokenRequest request)
        {
            //check tenant here
            //tenant key must be in header 
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("tenantId", user.TenantId.ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var key = System.Text.Encoding.ASCII.GetBytes("My_Secret_Key_AspNanoProject");
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.Email,
                    Id = user.Id,
                });
            }
            return Unauthorized();
        }




        //[HttpGet("GetNames")]
        //[AllowAnonymous]
        //public ActionResult GetNames() {
        //    List<string> MyList = new List<string>() { "tayyab","shahid"};

        //    return Ok(MyList);
        //}


        ////[Authorize(Roles = "SuperAdmin")]
        //[Authorize(Roles = "root")]
        //[HttpGet("GetNewNames")]
        //public ActionResult GetNewNames()
        //{
        //    List<string> MyList = new List<string>() { "tayyab1", "shahid1" };

        //    return Ok(MyList);
        //}


        //[AllowAnonymous]
        //[HttpPost("RegisterUser")]
        //public async Task<ActionResult> Register([FromBody] MyLoginModelType myLoginModel) {

        //    var userExist = await _userManager.FindByEmailAsync(myLoginModel.Email);
        //    if (userExist != null)
        //    {
        //        return Ok(new { Result = "User Already Exist" });
        //    }

        //    else {
        //        var user = new ApplicationUser
        //        {
        //            UserName = myLoginModel.Email,
        //            Email = myLoginModel.Email,
        //            EmailConfirmed = false
        //        };

        //        var result = await _userManager.CreateAsync(user, myLoginModel.Password);
        //        if (result.Succeeded)
        //        {


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));

        //            await _userManager.AddToRoleAsync(user, UserRoles.User);


        //            return Ok(new { Result = "User Created Successfully" });
        //        }
        //        else
        //        {
        //            StringBuilder stringBuilder = new StringBuilder();
        //            foreach (var error in result.Errors)
        //            {
        //                stringBuilder.Append(error.Description);
        //            }
        //            return Ok(new { Result = $"Refister Fail:{stringBuilder.ToString()}" });
        //        }
        //    }


        //}


        ////Register move to Identity Controller
        //[AllowAnonymous]
        //[HttpPost("registeradmin")]
        //public async Task<ActionResult> RegisterAdmin([FromBody] MyLoginModelType myLoginModel) //create DTOs for each request type -- in this case RegisterDTO
        //{

        //    var userExist = await _userManager.FindByEmailAsync(myLoginModel.Email);
        //    if (userExist != null)
        //    {
        //        return Ok(new { Result = "Admin Already Exist" });
        //    }

        //    else
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = myLoginModel.Email,
        //            Email = myLoginModel.Email,
        //            EmailConfirmed = false
        //        };

        //        var result = await _userManager.CreateAsync(user, myLoginModel.Password);
        //        if (result.Succeeded)
        //        {


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));

        //            await _userManager.AddToRoleAsync(user, UserRoles.Admin);


        //            return Ok(new { Result = "User Created Successfully" });
        //        }
        //        else
        //        {
        //            StringBuilder stringBuilder = new StringBuilder();
        //            foreach (var error in result.Errors)
        //            {
        //                stringBuilder.Append(error.Description);
        //            }
        //            return Ok(new { Result = $"Refister Fail:{stringBuilder.ToString()}" });
        //        }
        //    }


        //}



        //[AllowAnonymous]
        //[HttpPost("RegisterSuperAdmin")]
        //public async Task<ActionResult> RegisterSuperAdmin([FromBody] MyLoginModelType myLoginModel)
        //{

        //    var userExist = await _userManager.FindByEmailAsync(myLoginModel.Email);
        //    if (userExist != null)
        //    {
        //        return Ok(new { Result = "Super Admin Already Exist" });
        //    }

        //    else
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = myLoginModel.Email,
        //            Email = myLoginModel.Email,
        //            EmailConfirmed = false
        //        };

        //        var result = await _userManager.CreateAsync(user, myLoginModel.Password);
        //        if (result.Succeeded)
        //        {


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));


        //            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
        //                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));

        //            await _userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);


        //            return Ok(new { Result = "Super Admin Created Successfully" });
        //        }
        //        else
        //        {
        //            StringBuilder stringBuilder = new StringBuilder();
        //            foreach (var error in result.Errors)
        //            {
        //                stringBuilder.Append(error.Description);
        //            }
        //            return Ok(new { Result = $"Refister Fail:{stringBuilder.ToString()}" });
        //        }
        //    }


        //}




    }
}
