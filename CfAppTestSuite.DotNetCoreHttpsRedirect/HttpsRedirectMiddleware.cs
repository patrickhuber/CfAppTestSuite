using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CfAppTestSuite.DotNetCoreHttpsRedirect
{
    /// <summary>
    /// <see cref="https://gist.github.com/mattwhetton/72d1cd532115e103600b6b6cfd5c50dc"/>
    /// </summary>
    public class HttpsRedirectMiddleware
    {
        private RequestDelegate _next;

        public HttpsRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var protoHeader = context.Request.Headers["X-Forwarded-Proto"].ToString();
            if (context.Request.IsHttps || protoHeader.ToLower().Equals("https"))
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.Redirect($"https://{context.Request.Host}{context.Request.Path}");
            }
        }
    }

    public static class HttpsRedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpsRedirect(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpsRedirectMiddleware>();
        }
    }
}
