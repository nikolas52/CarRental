using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services.Interfaces;
using Wypozyczalnia.ViewModels;

namespace Wypozyczalnia.Controllers
{
    public class SamochodyController : Controller
    {
        private readonly ISamochodService _samochodService;

        public SamochodyController(ISamochodService samochodService)
        {
            _samochodService = samochodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var samochody = await _samochodService.GetAllSamochodyAsync();
            var samochodyVM = samochody.Select(s => new SamochodViewModel
            {
                IdSamochodu = s.IdSamochodu,
                Marka = s.Marka,
                Model = s.Model,
                RokProdukcji = s.RokProdukcji,
                Kolor = s.Kolor,
                Dostepnosc = s.Dostepnosc,
                PojSilnika = s.PojSilnika,
                MocSilnika = s.MocSilnika,
                CenaZaWypozyczenie = s.CenaZaWypozyczenie
            });
            return View(samochodyVM);
        }

        [HttpGet]
        public async Task<IActionResult> TopSamochody()
        {
            var topSamochody = await _samochodService.GetTopWypozyczaneSamochodyAsync();
            var topSamochodyVM = topSamochody.Take(5).Select(s => new TopWypozyczaneSamochodyViewModel
            {
                IdSamochodu = s.IdSamochodu,
                Marka = s.Marka,
                Model = s.Model,
                LiczbaWypozyczen = s.LiczbaWypozyczen
            });
            return View(topSamochodyVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SamochodViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SamochodViewModel samochodVM)
        {
            if (!ModelState.IsValid)
            {
                return View(samochodVM);
            }

            var samochod = new Samochod
            {
                Marka = samochodVM.Marka,
                Model = samochodVM.Model,
                RokProdukcji = samochodVM.RokProdukcji,
                Kolor = samochodVM.Kolor,
                Dostepnosc = samochodVM.Dostepnosc,
                PojSilnika = samochodVM.PojSilnika,
                MocSilnika = samochodVM.MocSilnika,
                CenaZaWypozyczenie = samochodVM.CenaZaWypozyczenie
            };
            await _samochodService.AddSamochodAsync(samochod);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var samochod = await _samochodService.GetSamochodByIdAsync(id);
            if (samochod == null) return NotFound();

            var samochodVM = new SamochodViewModel
            {
                IdSamochodu = samochod.IdSamochodu,
                Marka = samochod.Marka,
                Model = samochod.Model,
                RokProdukcji = samochod.RokProdukcji,
                Kolor = samochod.Kolor,
                Dostepnosc = samochod.Dostepnosc,
                PojSilnika = samochod.PojSilnika,
                MocSilnika = samochod.MocSilnika,
                CenaZaWypozyczenie = samochod.CenaZaWypozyczenie
            };
            return View(samochodVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SamochodViewModel samochodVM)
        {
            if (id != samochodVM.IdSamochodu) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(samochodVM);
            }

            var samochod = new Samochod
            {
                IdSamochodu = samochodVM.IdSamochodu,
                Marka = samochodVM.Marka,
                Model = samochodVM.Model,
                RokProdukcji = samochodVM.RokProdukcji,
                Kolor = samochodVM.Kolor,
                Dostepnosc = samochodVM.Dostepnosc,
                PojSilnika = samochodVM.PojSilnika,
                MocSilnika = samochodVM.MocSilnika,
                CenaZaWypozyczenie = samochodVM.CenaZaWypozyczenie
            };
            await _samochodService.UpdateSamochodAsync(samochod);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var samochod = await _samochodService.GetSamochodByIdAsync(id);
            if (samochod == null) return NotFound();

            var samochodVM = new SamochodViewModel
            {
                IdSamochodu = samochod.IdSamochodu,
                Marka = samochod.Marka,
                Model = samochod.Model,
                RokProdukcji = samochod.RokProdukcji,
                Kolor = samochod.Kolor,
                Dostepnosc = samochod.Dostepnosc,
                PojSilnika = samochod.PojSilnika,
                MocSilnika = samochod.MocSilnika,
                CenaZaWypozyczenie = samochod.CenaZaWypozyczenie
            };
            return View(samochodVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _samochodService.DeleteSamochodAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}