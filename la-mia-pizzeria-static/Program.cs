using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration.GetConnectionString("PizzeriaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzeriaContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<PizzaContext>(); //auth

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<PizzaContext>(); //aggiungo AddRoles<IdentityRole>() per i Ruoli

            builder.Services.AddRazorPages(); //auth

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Pizza/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization(); //auth indicare alla webapp che deve utilizzare l'autenticazione

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pizza}/{action=Index}/{id?}");

            app.MapRazorPages(); //auth

            app.Run();
        }
    }
}