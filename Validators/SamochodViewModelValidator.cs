using FluentValidation;
using Wypozyczalnia.ViewModels;

namespace Wypozyczalnia.Validators
{
    public class SamochodViewModelValidator : AbstractValidator<SamochodViewModel>
    {
        public SamochodViewModelValidator()
        {
            RuleFor(x => x.Marka)
                .NotEmpty().WithMessage("Marka jest wymagana")
                .MaximumLength(30).WithMessage("Marka nie może być dłuższa niż 30 znaków");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model jest wymagany")
                .MaximumLength(30).WithMessage("Model nie może być dłuższy niż 30 znaków");

            RuleFor(x => x.RokProdukcji)
                .NotEmpty().WithMessage("Rok produkcji jest wymagany")
                .InclusiveBetween(1910, DateTime.Now.Year).WithMessage($"Rok produkcji musi być między 1910 a {DateTime.Now.Year}");

            RuleFor(x => x.Kolor)
                .MaximumLength(20).WithMessage("Kolor nie może być dłuższy niż 20 znaków");

            RuleFor(x => x.PojSilnika)
                .NotEmpty().WithMessage("Pojemność silnika jest wymagana")
                .GreaterThan(0).WithMessage("Pojemność silnika musi być większa niż 0");

            RuleFor(x => x.MocSilnika)
                .NotEmpty().WithMessage("Moc silnika jest wymagana")
                .GreaterThan(0).WithMessage("Moc silnika musi być większa niż 0");

            RuleFor(x => x.CenaZaWypozyczenie)
                .NotEmpty().WithMessage("Cena za wypożyczenie jest wymagana")
                .GreaterThan(0).WithMessage("Cena za wypożyczenie musi być większa niż 0");
        }
    }
}