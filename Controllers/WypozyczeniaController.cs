using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Controllers
{
    public class WypozyczeniaController : Controller
    {
        private readonly IWypozyczenieRepository _wypozyczenieRepository;
        private readonly IKlientRepository _klientRepository;
        private readonly ISamochodRepository _samochodRepository;

        public WypozyczeniaController(IWypozyczenieRepository wypozyczenieRepository, IKlientRepository klientRepository, ISamochodRepository samochodRepository)
        {
            _wypozyczenieRepository = wypozyczenieRepository;
            _klientRepository = klientRepository;
            _samochodRepository = samochodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var wypozyczenia = await _wypozyczenieRepository.GetAllAsync();
            return View(wypozyczenia);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Klienci = await _klientRepository.GetAllAsync();
            ViewBag.Samochody = await _samochodRepository.GetAvailableAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKlienta,IdSamochodu,DataRozpoczecia,DataZakonczenia,Kwota")] Wypozyczenie wypozyczenie)
        {
            Console.WriteLine("Próba dodania nowego wypożyczenia:");
            Console.WriteLine($"IdKlienta: {wypozyczenie.IdKlienta}, IdSamochodu: {wypozyczenie.IdSamochodu}, DataRozpoczecia: {wypozyczenie.DataRozpoczecia}, DataZakonczenia: {wypozyczenie.DataZakonczenia}, Kwota: {wypozyczenie.Kwota}");

            // Usuwamy walidację właściwości nawigacyjnych
            ModelState.Remove("Klient");
            ModelState.Remove("Samochod");

            if (ModelState.IsValid)
            {
                try
                {
                    await _wypozyczenieRepository.AddAsync(wypozyczenie);
                    Console.WriteLine("Wypożyczenie zapisane pomyślnie!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Błąd podczas zapisywania wypożyczenia: {ex.Message}");
                    Console.WriteLine($"Błąd: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Pole: {error.Key}");
                    foreach (var err in error.Value.Errors)
                    {
                        Console.WriteLine($"Błąd walidacji: {err.ErrorMessage}");
                    }
                }
            }
            Console.WriteLine("Ponowne wyświetlenie formularza z błędami.");
            ViewBag.Klienci = await _klientRepository.GetAllAsync();
            ViewBag.Samochody = await _samochodRepository.GetAvailableAsync();
            return View(wypozyczenie);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var wypozyczenie = await _wypozyczenieRepository.GetByIdAsync(id);
            if (wypozyczenie == null) return NotFound();
            ViewBag.Klienci = await _klientRepository.GetAllAsync();
            ViewBag.Samochody = await _samochodRepository.GetAvailableAsync();
            return View(wypozyczenie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdWypozyczenia,IdKlienta,IdSamochodu,DataRozpoczecia,DataZakonczenia,Kwota")] Wypozyczenie wypozyczenie)
        {
            if (id != wypozyczenie.IdWypozyczenia) return NotFound();

            Console.WriteLine("Próba edycji wypożyczenia:");
            Console.WriteLine($"IdWypozyczenia: {wypozyczenie.IdWypozyczenia}, IdKlienta: {wypozyczenie.IdKlienta}, IdSamochodu: {wypozyczenie.IdSamochodu}, DataRozpoczecia: {wypozyczenie.DataRozpoczecia}, DataZakonczenia: {wypozyczenie.DataZakonczenia}, Kwota: {wypozyczenie.Kwota}");

            // Usuwamy walidację właściwości nawigacyjnych
            ModelState.Remove("Klient");
            ModelState.Remove("Samochod");

            if (ModelState.IsValid)
            {
                try
                {
                    await _wypozyczenieRepository.UpdateAsync(wypozyczenie);
                    Console.WriteLine("Wypożyczenie zaktualizowane pomyślnie!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Błąd podczas aktualizacji wypożyczenia: {ex.Message}");
                    Console.WriteLine($"Błąd: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Pole: {error.Key}");
                    foreach (var err in error.Value.Errors)
                    {
                        Console.WriteLine($"Błąd walidacji: {err.ErrorMessage}");
                    }
                }
            }
            Console.WriteLine("Ponowne wyświetlenie formularza edycji z błędami.");
            ViewBag.Klienci = await _klientRepository.GetAllAsync();
            ViewBag.Samochody = await _samochodRepository.GetAvailableAsync();
            return View(wypozyczenie);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var wypozyczenie = await _wypozyczenieRepository.GetByIdAsync(id);
            if (wypozyczenie == null)
            {
                Console.WriteLine($"Nie znaleziono wypożyczenia o ID: {id}");
                return NotFound();
            }
            Console.WriteLine($"Wyświetlono formularz usuwania dla wypożyczenia o ID: {id}");
            return View(wypozyczenie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdWypozyczenia)
        {
            Console.WriteLine($"Próba usunięcia wypożyczenia o ID: {IdWypozyczenia}");
            try
            {
                var wypozyczenie = await _wypozyczenieRepository.GetByIdAsync(IdWypozyczenia);
                if (wypozyczenie == null)
                {
                    Console.WriteLine($"Wypożyczenie o ID: {IdWypozyczenia} nie istnieje.");
                    return NotFound();
                }
                await _wypozyczenieRepository.DeleteAsync(IdWypozyczenia);
                Console.WriteLine($"Wypożyczenie o ID: {IdWypozyczenia} zostało usunięte pomyślnie.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas usuwania wypożyczenia: {ex.Message}\nStackTrace: {ex.StackTrace}");
                ModelState.AddModelError("", $"Błąd podczas usuwania wypożyczenia: {ex.Message}");
                var wypozyczenie = await _wypozyczenieRepository.GetByIdAsync(IdWypozyczenia);
                if (wypozyczenie == null)
                {
                    Console.WriteLine($"Po błędzie: Wypożyczenie o ID: {IdWypozyczenia} nie istnieje.");
                    return NotFound();
                }
                return View("Delete", wypozyczenie);
            }
        }
    }
}