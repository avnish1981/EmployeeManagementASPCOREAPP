using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementASPCOREAPP.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementASPCOREAPP.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration  config)
        {
            this._config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            //services.AddScoped<IEmployeeRepositary, SQLEmployeeRepository >();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddScoped<IEmployeeRepositary, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env  )
        {
            //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // developerExceptionPageOptions.SourceCodeLineCount = 1;
                
            }
            else
            {
                // app.UseStatusCodePages()
                // app.UseStatusCodePagesWithRedirects();
                app.UseExceptionHandler("/Error");
               // app.UseStatusCodePagesWithReExecute("/Error/{0}/"); // As soon as it reci
            }

            //app.UseDeveloperExceptionPage(developerExceptionPageOptions);

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute(); // Since URI foo/abc does not found  in Route , so it returns status code 404

            // This Midddle having default route configured.
            app.UseMvc(configureRoute =>
            {
                configureRoute.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });
                //app.UseMvc();
                //app.Run(async (context) =>
                //{

                //    await context.Response.WriteAsync("hello from enviorment :" + env.EnvironmentName );

                //});






            }
    }
}
