using AspNano.Infrastructure;
using AspNano.WebApi.Helper;
using AspNano.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


//Calling Helper method to Register services
builder.Services.ConfigureApplicationServices(builder.Configuration);


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

//Calling the middleware
//app.UseMiddleware<TenantMiddleware>();
app.TenantMW();

app.Run();