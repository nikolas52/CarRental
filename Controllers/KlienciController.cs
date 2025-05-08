using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services.Interfaces;
using AutoMapper;
using Wypozyczalnia.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Wypozyczalnia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KlienciController : Controller
    {
        private readonly IKlientService _klientService;
        private readonly IMapper _mapper;

        public KlienciController(IKlientService klientService, IMapper mapper)
        {
            _klientService = klientService;
            _mapper = mapper;
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

            var viewModels = _mapper.Map<IEnumerable<KlientViewModel>>(pagedResult.Items);
            var pagedViewModel = new PagedResult<KlientViewModel>
            {
                Items = viewModels,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };

            ViewBag.Nazwisko = nazwisko;

            return View(pagedViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("Wyświetlono formularz dodawania klienta.");
            return View(new KlientViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KlientViewModel viewModel)
        {
            Console.WriteLine("Próba dodania nowego klienta:");
            Console.WriteLine($"Imie: {viewModel.Imie}, Nazwisko: {viewModel.Nazwisko}, Email: {viewModel.Email}, Telefon: {viewModel.Telefon}, Adres: {viewModel.Adres}");

            if (ModelState.IsValid)
            {
                try
                {
                    var klient = _mapper.Map<Klient>(viewModel); // Mapowanie na model domenowy
                    klient.Wypozyczenia = new List<Wypozyczenie>(); 
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
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var klient = await _klientService.GetKlientByIdAsync(id);
            if (klient == null) return NotFound();
            var viewModel = _mapper.Map<KlientViewModel>(klient); 
            Console.WriteLine($"Wyświetlono formularz edycji dla klienta o ID: {id}");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KlientViewModel viewModel)
        {
            if (id != viewModel.IdKlienta) return NotFound();

            Console.WriteLine("Próba edycji klienta:");
            Console.WriteLine($"IdKlienta: {viewModel.IdKlienta}, Imie: {viewModel.Imie}, Nazwisko: {viewModel.Nazwisko}, Email: {viewModel.Email}, Telefon: {viewModel.Telefon}, Adres: {viewModel.Adres}");

            if (ModelState.IsValid)
            {
                try
                {
                    var klient = _mapper.Map<Klient>(viewModel); // Mapowanie na model domenowy
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
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var klient = await _klientService.GetKlientByIdAsync(id);
            if (klient == null) return NotFound();
            var viewModel = _mapper.Map<KlientViewModel>(klient); // Mapowanie na ViewModel
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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
                var viewModel = _mapper.Map<KlientViewModel>(klient); // Mapowanie na ViewModel
                return View("Delete", viewModel);
            }
        }
    }
}