using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public class WypozyczenieRepository : IWypozyczenieRepository
    {
        private readonly WypozyczalniaContext _context;

        public WypozyczenieRepository(WypozyczalniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Wypozyczenie>> GetAllAsync()
        {
            return await _context.Wypozyczenia
                .Include(w => w.Klient)
                .Include(w => w.Samochod)
                .ToListAsync();
        }

        public async Task<Wypozyczenie> GetByIdAsync(int id)
        {
            return await _context.Wypozyczenia
                .Include(w => w.Klient)
                .Include(w => w.Samochod)
                .FirstOrDefaultAsync(w => w.IdWypozyczenia == id);
        }

        public async Task AddAsync(Wypozyczenie wypozyczenie)
        {
            await _context.Wypozyczenia.AddAsync(wypozyczenie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wypozyczenie wypozyczenie)
        {
            _context.Wypozyczenia.Update(wypozyczenie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var wypozyczenie = await GetByIdAsync(id);
            if (wypozyczenie != null)
            {
                _context.Wypozyczenia.Remove(wypozyczenie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Wypozyczenie>> GetActiveWypozyczeniaAsync()
        {
            return await _context.Wypozyczenia
                .Include(w => w.Klient)
                .Include(w => w.Samochod)
                .Where(w => w.DataZakonczenia == null)
                .ToListAsync();
        }
    }
}