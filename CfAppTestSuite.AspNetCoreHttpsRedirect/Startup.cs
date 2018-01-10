using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace CfAppTestSuite.AspNetCoreHttpsRedirect
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("health", 
                context => 
                {
                    return context.Response.WriteAsync("healthy");
                });
            routeBuilder.MapGet("{*url}", 
                context=>
                {
                    // comment below if doing local development
                    if(!env.IsDevelopment())
                        EnforceHttps(context);
                    return context.Response.WriteAsync("Hello World!");
                });

            var routes = routeBuilder.Build();

            app.UseRouter(routes);
        }

        private static void EnforceHttps(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.IsHttps)
                return;
            
            if (string.Equals(
                request.Headers["X-Forwarded-Proto"],
                Uri.UriSchemeHttps,
                StringComparison.OrdinalIgnoreCase))
                return;
            
            var httpsUri = GetHttpsUri(request);

            response.Redirect(
                location: httpsUri.ToString(),
                permanent: true);
        }

        private static Uri GetHttpsUri(HttpRequest request)
        {
            var httpsUriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = request.Host.Host,
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };
            return httpsUriBuilder.Uri;
        }
    }
}
