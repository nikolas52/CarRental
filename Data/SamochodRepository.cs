using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public class SamochodRepository : ISamochodRepository
    {
        private readonly WypozyczalniaContext _context;

        public SamochodRepository(WypozyczalniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Samochod>> GetAllAsync()
        {
            return await _context.Samochody.ToListAsync();
        }

        public async Task<Samochod> GetByIdAsync(int id)
        {
            return await _context.Samochody.FindAsync(id);
        }

        public async Task AddAsync(Samochod samochod)
        {
            await _context.Samochody.AddAsync(samochod);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Samochod samochod)
        {
            _context.Samochody.Update(samochod);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var samochod = await GetByIdAsync(id);
            if (samochod != null)
            {
                _context.Samochody.Remove(samochod);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Samochod>> GetAvailableAsync()
        {
            return await _context.Samochody.Where(s => s.Dostepnosc).ToListAsync();
        }
    }
}