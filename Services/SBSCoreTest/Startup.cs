using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SBSCoreTest
{
    public class Startup
    {
        string name;

        public Startup()
        {
            name = "SBSCoreTest";
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            int x = 2;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            //app.Use
            //app.Map
            //app.MapWhen
            //app.UseWhen
            //app.UseMiddleware

            app.Run(async (context) =>
            {
                x = x + 2;
                await context.Response.WriteAsync($"x = {x}");
            });
        }
    }
}
