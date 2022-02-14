using AspNano.WebApi.Helper;
using AspNano.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


//Calling Helper method to Register services
builder.Services.ConfigureApplicationServices(builder.Configuration);

//Registering the Logger
builder.Logging.ConfigureLogging(builder.Configuration);

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
//app.UseMiddleware();
app.MapControllers();

app.Run();