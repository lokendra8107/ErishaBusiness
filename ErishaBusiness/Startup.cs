using Autofac;
using ErishaBusiness.Dependencies;
using ErishaBusiness.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region[START : APPLICATION KEYS FETCH FROM APPSETTING.JSON]
            SiteKeys.Configure(Configuration.GetSection("AppSettings"));
            var key = Encoding.ASCII.GetBytes(SiteKeys.Token);
            #endregion

            #region[START : ADDED SESSION FUNCTIONALITY WITH TIME LIMIT OF 60 MIN IF IDEAL]
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            #endregion

            services.AddTransient<IDbConnection>(db =>
            {
                var sqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                return sqlConnection;
            });


            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "Admin",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
         
        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new DomainServiceModule(Configuration));
        //}
    }
}
