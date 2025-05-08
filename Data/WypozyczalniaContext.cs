using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public class WypozyczalniaContext : IdentityDbContext<ApplicationUser>
    {
        public WypozyczalniaContext(DbContextOptions<WypozyczalniaContext> options)
            : base(options)
        {
        }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Samochod> Samochody { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
    }
}