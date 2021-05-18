using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TowTruckUberAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using TowTruckUberAPI.Infrastructure;
using TowTruckUberAPI.Infrastructure.Database;

namespace TowTruckUberAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); //TODO: to, czy AddMvcCore() czy bez niczego
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));


            // For Identity  
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddDataProtection()
                // This helps surviving a restart: a same app will find back its keys. Just ensure to create the folder.
                .PersistKeysToFileSystem(new DirectoryInfo("\\MyFolder\\keys\\"))
                // This helps surviving a site update: each app has its own store, building the site creates a new app
                .SetApplicationName("MyWebsite")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

            // CORS
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()));


            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<AppDbContext>().Database.EnsureCreated();

            SeedData.Seed(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            //dodane



            //app.UseHttpsRedirection();
            //app.UseAuthentication();
            //app.UseStaticFiles();
            //app.UseRouting();

            //app.UseCors(builder =>
            //    builder
            //        .AllowAnyOrigin()
            //        .AllowAnyHeader()
            //        .AllowAnyMethod());

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseRouting();





            app.UseAuthorization();



            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


    }
}

