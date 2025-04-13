using FluentValidation;
using Wypozyczalnia.ViewModels;

namespace Wypozyczalnia.Validators
{
    public class TopWypozyczaneSamochodyViewModelValidator : AbstractValidator<TopWypozyczaneSamochodyViewModel>
    {
        public TopWypozyczaneSamochodyViewModelValidator()
        {
            RuleFor(x => x.Marka)
                .NotEmpty().WithMessage("Marka jest wymagana")
                .MaximumLength(30).WithMessage("Marka nie może być dłuższa niż 50 znaków");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model jest wymagany")
                .MaximumLength(30).WithMessage("Model nie może być dłuższy niż 50 znaków");

            RuleFor(x => x.LiczbaWypozyczen)
                .GreaterThanOrEqualTo(0).WithMessage("Liczba wypożyczeń nie może być ujemna");
        }
    }
}