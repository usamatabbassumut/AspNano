using Serilog;

namespace AspNano.WebApi.Helper
{
    public static class RegisterLogger
    {
        public static void ConfigureLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
            
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);
        }
    }
}
