using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementASPCOREAPP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
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
            //Registrying Enity Framework
            services.AddDbContextPool<AppDbContext>(options =>options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            //REgistring Identity Services.
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.MaxFailedAccessAttempts = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<AppDbContext>();
          /*  services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;

            });*/
            services.AddScoped<IEmployeeRepositary, SQLEmployeeRepository >();
            services.AddMvc(options=> 
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddXmlSerializerFormatters();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env  )
        {
            //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                //app.UseStatusCodePagesWithReExecute("/Error/{0}/");
                // developerExceptionPageOptions.SourceCodeLineCount = 1;

            }
            else if(env.IsProduction())
            {
                // app.UseStatusCodePages()
                // app.UseStatusCodePagesWithRedirects();
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}/"); // As soon as it reci
            }

            //app.UseDeveloperExceptionPage(developerExceptionPageOptions);

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute(); // Since URI foo/abc does not found  in Route , so it returns status code 404
            app.UseAuthentication();//This Middle ware enable to use Identity Security Services.
            // This Midddle having default route configured.
            app.UseMvc(Route =>
            {
                Route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });
                //app.UseMvc();
                //app.Run(async (context) =>
                //{

                //    await context.Response.WriteAsync("hello from enviorment :" + env.EnvironmentName );

                //});






            }
    }
}
