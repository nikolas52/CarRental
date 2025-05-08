using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services.Interfaces;

namespace Wypozyczalnia.Services
{
    public class SamochodService : ISamochodService
    {
        private readonly ISamochodRepository _samochodRepository;
        private readonly IWypozyczenieRepository _wypozyczenieRepository;

        public SamochodService(ISamochodRepository samochodRepository, IWypozyczenieRepository wypozyczenieRepository)
        {
            _samochodRepository = samochodRepository;
            _wypozyczenieRepository = wypozyczenieRepository;
        }

        public async Task<IEnumerable<Samochod>> GetAllSamochodyAsync()
        {
            return await _samochodRepository.GetAllAsync();
        }

        public async Task<Samochod> GetSamochodByIdAsync(int id)
        {
            return await _samochodRepository.GetByIdAsync(id);
        }

        public async Task<Samochod> AddSamochodAsync(Samochod samochod)
        {
            await _samochodRepository.AddAsync(samochod);
            return samochod;
        }

        public async Task UpdateSamochodAsync(Samochod samochod)
        {
            await _samochodRepository.UpdateAsync(samochod);
        }

        public async Task DeleteSamochodAsync(int id)
        {
            await _samochodRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Samochod>> GetAvailableSamochodyAsync()
        {
            return await _samochodRepository.GetAvailableAsync();
        }

        public async Task<IEnumerable<TopWypozyczaneSamochodyDto>> GetTopWypozyczaneSamochodyAsync()
        {
            var wypozyczenia = await _wypozyczenieRepository.GetAllAsync();
            var topSamochody = wypozyczenia
                .GroupBy(w => w.IdSamochodu)
                .Select(g => new TopWypozyczaneSamochodyDto
                {
                    IdSamochodu = g.Key,
                    Marka = g.First().Samochod.Marka,
                    Model = g.First().Samochod.Model,
                    LiczbaWypozyczen = g.Count()
                })
                .OrderByDescending(t => t.LiczbaWypozyczen)
                .Take(5); 

            return topSamochody;
        }


    }
}