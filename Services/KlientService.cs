using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services.Interfaces;

namespace Wypozyczalnia.Services
{
    public class KlientService : IKlientService
    {
        private readonly IKlientRepository _klientRepository;

        public KlientService(IKlientRepository klientRepository)
        {
            _klientRepository = klientRepository;
        }

        public async Task<IEnumerable<Klient>> GetAllKlienciAsync(int pageNumber, int pageSize)
        {
            var klienci = await _klientRepository.GetAllAsync();
            return klienci
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task<Klient> GetKlientByIdAsync(int id)
        {
            return await _klientRepository.GetByIdAsync(id);
        }

        public async Task<Klient> AddKlientAsync(Klient klient)
        {
            await _klientRepository.AddAsync(klient);
            return klient;
        }

        public async Task UpdateKlientAsync(Klient klient)
        {
            await _klientRepository.UpdateAsync(klient);
        }

        public async Task DeleteKlientAsync(int id)
        {
            await _klientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Klient>> SearchKlientByNazwiskoAsync(string nazwisko, int pageNumber, int pageSize)
        {
            var klienci = await _klientRepository.GetAllAsync();
            return klienci
                .Where(k => k.Nazwisko.Contains(nazwisko, StringComparison.OrdinalIgnoreCase))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task<int> GetTotalKlienciCountAsync()
        {
            var klienci = await _klientRepository.GetAllAsync();
            return klienci.Count();
        }
    }
}