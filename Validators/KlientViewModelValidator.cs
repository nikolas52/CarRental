using FluentValidation;
using Wypozyczalnia.ViewModels;

namespace Wypozyczalnia.Validators
{
    public class KlientViewModelValidator : AbstractValidator<KlientViewModel>
    {
        public KlientViewModelValidator()
        {
            RuleFor(x => x.Imie)
                .NotEmpty().WithMessage("Imię jest wymagane")
                .MaximumLength(50).WithMessage("Imię nie może być dłuższe niż 50 znaków");

            RuleFor(x => x.Nazwisko)
                .NotEmpty().WithMessage("Nazwisko jest wymagane")
                .MaximumLength(50).WithMessage("Nazwisko nie może być dłuższe niż 50 znaków");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany")
                .EmailAddress().WithMessage("Podaj poprawny adres email");

            RuleFor(x => x.Telefon)
                .NotEmpty().WithMessage("Telefon jest wymagany")
                .Matches(@"^\+?\d{9,15}$").WithMessage("Podaj poprawny numer telefonu");

            RuleFor(x => x.Adres)
                .NotEmpty().WithMessage("Adres jest wymagany")
                .MaximumLength(100).WithMessage("Adres nie może być dłuższy niż 100 znaków");
        }
    }
}