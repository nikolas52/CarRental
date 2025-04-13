using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services.Interfaces;

namespace Wypozyczalnia.Controllers
{
    public class KlienciController : Controller
    {
        private readonly IKlientService _klientService;

        public KlienciController(IKlientService klientService)
        {
            _klientService = klientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string nazwisko, int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Klient> klienci;
            int totalCount;

            if (string.IsNullOrEmpty(nazwisko))
            {
                klienci = await _klientService.GetAllKlienciAsync(pageNumber, pageSize);
                totalCount = await _klientService.GetTotalKlienciCountAsync();
            }
            else
            {
                klienci = await _klientService.SearchKlientByNazwiskoAsync(nazwisko, pageNumber, pageSize);
                totalCount = (await _klientService.SearchKlientByNazwiskoAsync(nazwisko, 1, int.MaxValue)).Count();
            }

            var pagedResult = new PagedResult<Klient>
            {
                Items = klienci,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            Console.WriteLine($"Liczba klientów w bazie: {totalCount}, Strona: {pageNumber}");

            ViewBag.Nazwisko = nazwisko;

            return View(pagedResult);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("Wyświetlono formularz dodawania klienta.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Imie,Nazwisko,Email,Telefon,Adres")] Klient klient)
        {
            Console.WriteLine("Próba dodania nowego klienta:");
            Console.WriteLine($"Imie: {klient.Imie}, Nazwisko: {klient.Nazwisko}, Email: {klient.Email}, Telefon: {klient.Telefon}, Adres: {klient.Adres}");

            if (ModelState.IsValid)
            {
                try
                {
                    await _klientService.AddKlientAsync(klient);
                    Console.WriteLine("Klient zapisany pomyślnie!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Błąd podczas zapisywania klienta: {ex.Message}");
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
            return View(klient);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var klient = await _klientService.GetKlientByIdAsync(id);
            if (klient == null) return NotFound();
            Console.WriteLine($"Wyświetlono formularz edycji dla klienta o ID: {id}");
            return View(klient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKlienta,Imie,Nazwisko,Email,Telefon,Adres")] Klient klient)
        {
            if (id != klient.IdKlienta) return NotFound();

            Console.WriteLine("Próba edycji klienta:");
            Console.WriteLine($"IdKlienta: {klient.IdKlienta}, Imie: {klient.Imie}, Nazwisko: {klient.Nazwisko}, Email: {klient.Email}, Telefon: {klient.Telefon}, Adres: {klient.Adres}");

            if (ModelState.IsValid)
            {
                try
                {
                    await _klientService.UpdateKlientAsync(klient);
                    Console.WriteLine("Klient zaktualizowany pomyślnie!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Błąd podczas aktualizacji klienta: {ex.Message}");
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
            return View(klient);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var klient = await _klientService.GetKlientByIdAsync(id);
            if (klient == null) return NotFound();
            return View(klient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _klientService.DeleteKlientAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Błąd podczas usuwania klienta: {ex.Message}");
                var klient = await _klientService.GetKlientByIdAsync(id);
                if (klient == null) return NotFound();
                return View("Delete", klient);
            }
        }
    }
}