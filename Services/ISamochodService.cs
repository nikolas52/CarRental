using System.Collections.Generic;
using System.Threading.Tasks;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services.Interfaces
{
    public interface ISamochodService
    {

        Task<IEnumerable<Samochod>> GetAllSamochodyAsync();
        Task<Samochod> GetSamochodByIdAsync(int id);
        Task<Samochod> AddSamochodAsync(Samochod samochod);
        Task UpdateSamochodAsync(Samochod samochod);
        Task DeleteSamochodAsync(int id);

        Task<IEnumerable<Samochod>> GetAvailableSamochodyAsync();
        Task<IEnumerable<TopWypozyczaneSamochodyDto>> GetTopWypozyczaneSamochodyAsync();
    }


    public class TopWypozyczaneSamochodyDto
    {
        public int IdSamochodu { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int LiczbaWypozyczen { get; set; }
    }
}