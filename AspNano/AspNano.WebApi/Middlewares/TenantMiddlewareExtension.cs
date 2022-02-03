namespace AspNano.WebApi.Middlewares
{
    public static class TenantMiddlewareExtension
    {
        public static IApplicationBuilder TenantMW(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();
            return app;
        }
    }
}
