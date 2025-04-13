using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public class KlientRepository : IKlientRepository
    {
        private readonly WypozyczalniaContext _context;

        public KlientRepository(WypozyczalniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Klient>> GetAllAsync()
        {
            return await _context.Klienci.ToListAsync();
        }

        public async Task<Klient> GetByIdAsync(int id)
        {
            return await _context.Klienci.FindAsync(id);
        }

        public async Task AddAsync(Klient klient)
        {
            await _context.Klienci.AddAsync(klient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Klient klient)
        {
            var existingKlient = await _context.Klienci.FindAsync(klient.IdKlienta);
            if (existingKlient == null)
            {
                throw new KeyNotFoundException($"Klient o ID {klient.IdKlienta} nie istnieje.");
            }

            existingKlient.Imie = klient.Imie;
            existingKlient.Nazwisko = klient.Nazwisko;
            existingKlient.Email = klient.Email;
            existingKlient.Telefon = klient.Telefon;
            existingKlient.Adres = klient.Adres;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var klient = await GetByIdAsync(id);
            if (klient != null)
            {
                _context.Klienci.Remove(klient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Klient> GetByEmailAsync(string email)
        {
            return await _context.Klienci.FirstOrDefaultAsync(k => k.Email == email);
        }
    }
}