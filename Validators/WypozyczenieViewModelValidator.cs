using FluentValidation;
using Wypozyczalnia.ViewModels;

namespace Wypozyczalnia.Validators
{
    public class WypozyczenieViewModelValidator : AbstractValidator<WypozyczenieViewModel>
    {
        public WypozyczenieViewModelValidator()
        {
            RuleFor(x => x.IdKlienta)
                .GreaterThan(0).WithMessage("Klient jest wymagany");

            RuleFor(x => x.IdSamochodu)
                .GreaterThan(0).WithMessage("Samochód jest wymagany");

            RuleFor(x => x.DataRozpoczecia)
                .NotEmpty().WithMessage("Data rozpoczęcia jest wymagana")
                .LessThanOrEqualTo(x => x.DataZakonczenia).WithMessage("Data rozpoczęcia musi być przed datą zakończenia");

            RuleFor(x => x.DataZakonczenia)
                .NotEmpty().WithMessage("Data zakończenia jest wymagana")
                .GreaterThanOrEqualTo(x => x.DataRozpoczecia).WithMessage("Data zakończenia musi być po dacie rozpoczęcia");

            RuleFor(x => x.Kwota)
                .NotEmpty().WithMessage("Kwota jest wymagana")
                .GreaterThan(0).WithMessage("Kwota musi być większa niż 0");
        }
    }
}