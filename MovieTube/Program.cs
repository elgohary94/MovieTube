global using MovieTube.Models;
global using MovieTube.ViewModels;
global using MovieTube.Controllers.Repositories;
global using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieTube.Profiles;
using Serilog;
using Microsoft.AspNetCore.Identity;
using MovieTube.Repositories;

namespace MovieTube
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var Configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            Log.Logger = new LoggerConfiguration()
                             .ReadFrom
                             .Configuration(Configuration)
                             .CreateLogger();

            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MovieDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                
            });

            //configuring identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MovieDbContext>();

            builder.Host.UseSerilog();
            builder.Services.AddAutoMapper(typeof(MappingConfiguering));

            builder.Services.AddScoped<IUserMovieRepository, UserMovieRepository>();
            builder.Services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}