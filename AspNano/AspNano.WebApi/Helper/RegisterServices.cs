using AspNano.Application.EFRepository;
using AspNano.Application.Repository.VenueRepository;
using AspNano.Infrastructure.Multitenancy;
using AspNano.Application.Services.VenueService;
using AspNano.Infrastructure.Persistence;
using AspNano.WebApi.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AspNano.WebApi.Helper
{
    public static class RegisterServices
    {

        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region [-- REGISTERING DB CONTEXT SERVICE --]
            //this resolved the circular issue -- separate DBcontexts
            services.AddDbContext<TenantManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); 
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region [-- SETTING UP IDENTITY CONFIGURATIONS --]

            //Idnetity Configuration
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //configurations
                options.SignIn.RequireConfirmedAccount = false;
            }
            ).AddEntityFrameworkStores<ApplicationDbContext>() 
             .AddDefaultTokenProviders();

            #endregion

            #region [-- JWT SETTINGS --]
            var key = System.Text.Encoding.ASCII.GetBytes("My_Secret_Key_AspNanoProject");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userMachine = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var user = userMachine.GetUserAsync(context.HttpContext.User);
                        if (user == null)
                        {
                            context.Fail("UnAuthorised");
                        }
                        return Task.CompletedTask;
                    }
                };
                //x.MapInboundClaims = false;
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });


            #endregion

            #region [-- SWAGGER SETTINGS --]
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

            });
            #endregion

            #region [-- REGISTERING SERVICES --]
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(typeof(ProfileMapper));
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IVenueService, VenueService>();



            #endregion

            #region [-- REGISTERING REPOSITORIES --]
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IVenueRepository, VenueRepository>();
            #endregion
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
