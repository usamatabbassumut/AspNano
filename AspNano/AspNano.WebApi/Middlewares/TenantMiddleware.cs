using AspNano.Common.HelperClasses;
using AspNano.DTOs.ResponseDTOs;
using AspNano.Infrastructure.Multitenancy;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace AspNano.WebApi.Middlewares
{
    public class TenantMiddleware
    {

        //NOT USED
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                //Resolve from user auth (token) or from header (header on login)
                string tenantId = TenantResolver.Resolver(httpContext);
                if (!string.IsNullOrEmpty(tenantId))
                {
                    //currentTenantService.SetCurrentTenant(tenantId);
                }

                //TenantUserInfo.TenantID= httpContext.User?.Claims?.FirstOrDefault(x => x.Type == "tenantId")?.Value;
                //TenantUserInfo.UserID= httpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
