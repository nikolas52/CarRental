using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public interface IKlientRepository
    {
        Task<IEnumerable<Klient>> GetAllAsync();
        Task<Klient> GetByIdAsync(int id);
        Task AddAsync(Klient klient);
        Task UpdateAsync(Klient klient);
        Task DeleteAsync(int id);
        Task<Klient> GetByEmailAsync(string email);
    }
}