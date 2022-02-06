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

                var key = Encoding.ASCII.GetBytes("My_Secret_Key_AspNanoProject");
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

    }
}
