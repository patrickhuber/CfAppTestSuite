﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CfAppTestSuite.RequestHeaders
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsync("[");

                var i = 0;
                foreach (var header in context.Request.Headers)
                {
                    if (i > 0)
                        await context.Response.WriteAsync(",");
                    await context.Response.WriteAsync($"{{\"key\":\"{header.Key}\",\"value\":\"{header.Value}\"}}");
                    i++;
                }

                await context.Response.WriteAsync("]");
            });
        }
    }
}
