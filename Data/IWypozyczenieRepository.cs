using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public interface IWypozyczenieRepository
    {
        Task<IEnumerable<Wypozyczenie>> GetAllAsync();
        Task<Wypozyczenie> GetByIdAsync(int id);
        Task AddAsync(Wypozyczenie wypozyczenie);
        Task UpdateAsync(Wypozyczenie wypozyczenie);
        Task DeleteAsync(int id);
        Task<IEnumerable<Wypozyczenie>> GetActiveWypozyczeniaAsync();
    }
}