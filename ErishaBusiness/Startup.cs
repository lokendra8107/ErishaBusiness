using Autofac;
using ErishaBusiness.Dependencies;
using ErishaBusiness.ImageResizer.Helpers;
using ErishaBusiness.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        IWebHostEnvironment _webHostEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(_ => new PhysicalFileProvider(_env.WebRootPath ?? _env.ContentRootPath));
            services.AddMemoryCache();
            services.AddImageResizer();

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

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            #region[START : JWT TOKEN USES , AUTHENTICATION AND AUTHORIZATION]
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
             {
                 token.RequireHttpsMetadata = false;
                 token.SaveToken = true;
                 token.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = true,
                     ValidIssuer = SiteKeys.Domain,
                     ValidateAudience = true,
                     ValidAudience = SiteKeys.Domain,
                     RequireExpirationTime = true,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.FromDays(1)
                 };
             });
            services.AddAuthentication().AddCookie(options =>
            {
                options.LoginPath = "/admin/account";
                options.LogoutPath = "/logout";
            });
            #endregion

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            app.UseImageResizer();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 365;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            #region[START : JWT TOKEN USES , AUTHENTACTION AND AUTHORIZATION]
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "Admin",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Account}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DomainServiceModule(Configuration));
        }
    }
}
