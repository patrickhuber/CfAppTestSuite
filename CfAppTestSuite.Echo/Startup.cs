using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace CfAppTestSuite.Echo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = context.Request.Headers.ContainsKey("accept")
                ? context.Request.Headers["accept"].First()
                : "text/html";

                if (context.Request.Method == HttpMethods.Get)
                {
                    await context.Response.WriteAsync(context.Request.QueryString.Value);
                }
                else if (context.Request.Method == HttpMethods.Post
                    || context.Request.Method == HttpMethods.Put)
                {
                    var bytesRead = 0;
                    var buffer = new byte[1024];
                    do
                    {
                        bytesRead = await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                        var data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        await context.Response.WriteAsync(data);
                    } while (bytesRead > 0);
                }
                else
                {
                    await context.Response.WriteAsync($"{context.Request.Method}");
                }
            });
        }
    }
}
