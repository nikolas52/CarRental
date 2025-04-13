using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Services.Interfaces;
using Wypozyczalnia.Services;
using FluentValidation.AspNetCore;
using Wypozyczalnia.Validators;

namespace ProjPM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<SamochodViewModelValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<KlientViewModelValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<WypozyczenieViewModelValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<TopWypozyczaneSamochodyViewModelValidator>();
                    fv.ImplicitlyValidateChildProperties = true;
                });

            // Rejestracja kontekstu bazy danych
            builder.Services.AddDbContext<Wypozyczalnia.Data.WypozyczalniaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Rejestracja repozytoriów
            builder.Services.AddScoped<ISamochodRepository, SamochodRepository>();
            builder.Services.AddScoped<IKlientRepository, KlientRepository>();
            builder.Services.AddScoped<IWypozyczenieRepository, WypozyczenieRepository>();

            builder.Services.AddScoped<IKlientService, KlientService>();
            builder.Services.AddScoped<ISamochodService, SamochodService>();
            builder.Services.AddScoped<IWypozyczenieService, WypozyczenieService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}