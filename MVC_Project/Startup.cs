using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Project_BLL.Interfaces;
using MVC_Project_BLL.Repositories;
using MVC_Project_DAL.Data;
using MVC_Project_DAL.Models;
using MVC_Project_PL.Extentions;
using MVC_Project_PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project
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
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });      
            services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            services.AddApplicationServices();
            services.AddIdentity<ApplicationUser,IdentityRole>(
            //Config => {
            //    Config.Password.RequiredUniqueChars = 2;
            //    Config.Password.RequireNonAlphanumeric = true;
            //    Config.Password.RequiredLength = 128;
            //    Config.Password.RequireDigit = true;
            //    Config.Password.RequireLowercase = true;
            //    Config.Password.RequireUppercase = true;
            //    Config.Lockout.MaxFailedAccessAttempts = 3;
            //    Config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            //    }    
            ).AddEntityFrameworkStores<AppDbContext>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
