using AspNano.Common.HelperClasses;
using AspNano.DTOs.ResponseDTOs;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace AspNano.WebApi.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                TenantUserInfo.TenantID= httpContext.User?.Claims?.FirstOrDefault(x => x.Type == "tenantId")?.Value;
                TenantUserInfo.UserID= httpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
