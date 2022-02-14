using Microsoft.AspNetCore.Http;
using AspNano.Infrastructure.Identity;

namespace AspNano.Infrastructure.Multitenancy
{

    public static class TenantResolver
    {
        public static string Resolver(HttpContext context)
        {
            string tenantId = ResolveFromUserAuth(context);
            if (!string.IsNullOrEmpty(tenantId))
            {
                return tenantId;
            }

            tenantId = ResolveFromHeader(context);
            if (!string.IsNullOrEmpty(tenantId))
            {
                return tenantId;
            }



            return default;
        }

        private static string ResolveFromUserAuth(HttpContext context)
        {
            // var test = context.User.FindFirst("tenant").Value;
            var test = context.User?.Claims?.FirstOrDefault(x => x.Type == "tenant")?.Value;
            return test;
        }

        private static string ResolveFromHeader(HttpContext context)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
            return tenantFromHeader;
        }

  
    }
}
