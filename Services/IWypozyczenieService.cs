using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services.Interfaces
{
    public interface IWypozyczenieService
    {
        Task<IEnumerable<Wypozyczenie>> GetAllWypozyczeniaAsync();
        Task<Wypozyczenie> GetWypozyczenieByIdAsync(int id);
        Task<Wypozyczenie> AddWypozyczenieAsync(Wypozyczenie wypozyczenie);
        Task UpdateWypozyczenieAsync(Wypozyczenie wypozyczenie);
        Task DeleteWypozyczenieAsync(int id);

        Task<IEnumerable<Wypozyczenie>> GetActiveWypozyczeniaAsync();

    }
}