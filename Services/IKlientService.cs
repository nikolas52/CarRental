using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services.Interfaces
{
    public interface IKlientService
    {
        Task<IEnumerable<Klient>> GetAllKlienciAsync(int pageNumber, int pageSize);
        Task<Klient> GetKlientByIdAsync(int id);
        Task<Klient> AddKlientAsync(Klient klient);
        Task UpdateKlientAsync(Klient klient);
        Task DeleteKlientAsync(int id);
        Task<IEnumerable<Klient>> SearchKlientByNazwiskoAsync(string nazwisko, int pageNumber, int pageSize);
        Task<int> GetTotalKlienciCountAsync();
    }
}