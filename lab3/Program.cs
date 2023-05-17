using lab3.Models;
using lab3.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace lab3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
			builder.Services.AddTransient<IDepartment, DepartmentMoc>();
			builder.Services.AddTransient<IStudent, StudentMoc>();
            builder.Services.AddDbContext<ITIContext>();
            builder.Services.AddSession(a =>
            {
                a.Cookie.Name = "MySession";
             

            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(a =>
                    {
                         a.LoginPath = "/Account/Index";
                    });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

/*			app.Use(async (context, next) => {

				await context.Response.WriteAsync("welcome\n");
				await next();
				await context.Response.WriteAsync("\n alex");
			});


			app.Run(async context => {

				await context.Response.WriteAsync("mvc");
			});*/

			app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}