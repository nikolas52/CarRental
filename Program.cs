using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Services.Interfaces;
using Wypozyczalnia.Services;
using FluentValidation.AspNetCore;
using Wypozyczalnia.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Wypozyczalnia.Models;
using Wypozyczalnia.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Wypozyczalnia.Utilities;

namespace ProjPM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Rejestracja AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Rejestracja FluentValidation
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
            builder.Services.AddDbContext<WypozyczalniaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Rejestracja repozytoriów
            builder.Services.AddScoped<ISamochodRepository, SamochodRepository>();
            builder.Services.AddScoped<IKlientRepository, KlientRepository>();
            builder.Services.AddScoped<IWypozyczenieRepository, WypozyczenieRepository>();

            // Rejestracja usług
            builder.Services.AddScoped<IKlientService, KlientService>();
            builder.Services.AddScoped<ISamochodService, SamochodService>();
            builder.Services.AddScoped<IWypozyczenieService, WypozyczenieService>();

            // Konfiguracja Identity z rolami
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() // Dodanie obsługi ról
                .AddEntityFrameworkStores<WypozyczalniaContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Inicjalizacja ról i użytkownika Administratora
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    RoleInitializer.InitializeAsync(serviceProvider).Wait();
                }
                catch (Exception ex)
                {
                    // Logowanie błędu (w rzeczywistej aplikacji użyj loggera)
                    Console.WriteLine($"Błąd podczas inicjalizacji ról i użytkownika: {ex.Message}");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
            app.MapRazorPages();

            app.Run();
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Klient, KlientViewModel>().ReverseMap();
        }
    }
}