using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services.Interfaces;

namespace Wypozyczalnia.Services
{
    public class WypozyczenieService : IWypozyczenieService
    {
        private readonly IWypozyczenieRepository _wypozyczenieRepository;
        private readonly ISamochodRepository _samochodRepository;

        public WypozyczenieService(IWypozyczenieRepository wypozyczenieRepository, ISamochodRepository samochodRepository)
        {
            _wypozyczenieRepository = wypozyczenieRepository;
            _samochodRepository = samochodRepository;
        }

        public async Task<IEnumerable<Wypozyczenie>> GetAllWypozyczeniaAsync()
        {
            return await _wypozyczenieRepository.GetAllAsync();
        }

        public async Task<Wypozyczenie> GetWypozyczenieByIdAsync(int id)
        {
            return await _wypozyczenieRepository.GetByIdAsync(id);
        }

        public async Task<Wypozyczenie> AddWypozyczenieAsync(Wypozyczenie wypozyczenie)
        {
            var samochod = await _samochodRepository.GetByIdAsync(wypozyczenie.IdSamochodu);
            if (samochod != null && samochod.Dostepnosc)
            {
                samochod.Dostepnosc = false;
                await _samochodRepository.UpdateAsync(samochod);
            }

            await _wypozyczenieRepository.AddAsync(wypozyczenie);
            return wypozyczenie;
        }

        public async Task UpdateWypozyczenieAsync(Wypozyczenie wypozyczenie)
        {
            await _wypozyczenieRepository.UpdateAsync(wypozyczenie);
        }

        public async Task DeleteWypozyczenieAsync(int id)
        {
            var wypozyczenie = await _wypozyczenieRepository.GetByIdAsync(id);
            if (wypozyczenie != null)
            {
                var samochod = await _samochodRepository.GetByIdAsync(wypozyczenie.IdSamochodu);
                if (samochod != null)
                {
                    samochod.Dostepnosc = true;
                    await _samochodRepository.UpdateAsync(samochod);
                }
                await _wypozyczenieRepository.DeleteAsync(id);
            }
        }

        public async Task<IEnumerable<Wypozyczenie>> GetActiveWypozyczeniaAsync()
        {
            return await _wypozyczenieRepository.GetActiveWypozyczeniaAsync();
        }
    }
}