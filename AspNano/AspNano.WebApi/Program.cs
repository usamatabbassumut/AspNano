using AspNano.Application.Repositories;
using AspNano.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Start of Configure Services


#region Dependence Injection
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
#endregion

#region Sql Connected

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion


#region Setting up Identity Configurations
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
        options => {
            options.SignIn.RequireConfirmedAccount = false;

            //Other options go here
        }
        )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region JWT Settings
var key = System.Text.Encoding.ASCII.GetBytes("My_Secret_Key_AspNanoProject");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(x=>{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userMachine = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var user= userMachine.GetUserAsync(context.HttpContext.User);
            if (user == null) {
                context.Fail("UnAuthorised");
            }
            return Task.CompletedTask;
        }
    };

    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
       ValidAudience=builder.Configuration["JWT:ValidAudience"],
       ValidIssuer =builder.Configuration["JWT:ValidIssuer"],
       IssuerSigningKey=new SymmetricSecurityKey(key),

    
    };
});
#endregion

#region Swagger Settings
builder.Services.AddSwaggerGen(setup =>
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




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Start of Configure Services
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

