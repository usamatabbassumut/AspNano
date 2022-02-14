namespace AspNano.WebApi.Middlewares
{
    public static class RegisterMiddlewareExtension
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();
            return app;
        }
    }
}
