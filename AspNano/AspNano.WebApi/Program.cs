using AspNano.WebApi.Helper;
using AspNano.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


//Calling Helper method to Register services
builder.Services.ConfigureApplicationServices(builder.Configuration);

#region [-- REGISTERING SERILOG FOR LOGGING --]
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion 

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