using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public interface ISamochodRepository
    {
        Task<IEnumerable<Samochod>> GetAllAsync();
        Task<Samochod> GetByIdAsync(int id);
        Task AddAsync(Samochod samochod);
        Task UpdateAsync(Samochod samochod);
        Task DeleteAsync(int id);
        Task<IEnumerable<Samochod>> GetAvailableAsync();
    }
}