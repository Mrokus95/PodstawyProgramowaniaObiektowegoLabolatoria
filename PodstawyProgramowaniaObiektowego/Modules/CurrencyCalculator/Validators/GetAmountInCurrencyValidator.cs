using FluentValidation;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Data;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.DTOs;

namespace PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Validators;

public abstract class GetAmountInCurrencyValidator : AbstractValidator<GetAmountInCurrency>
{
    public GetAmountInCurrencyValidator()
    {
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Pole 'Currency' nie może być puste.")
            .Must(BeValidCurrency)
            .WithMessage("Nieobsługiwany kod waluty. Dozwolone wartości: EUR, GBP, CHF.");

        RuleFor(x => x.Amount)
            .NotNull().WithMessage("Pole 'Amount' nie może być puste.")
            .GreaterThan(0).WithMessage("Kwota musi być większa niż 0.");
    }

    private bool BeValidCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            return false;
        
        return Enum.TryParse(typeof(Currencies), currency, true, out _);
    }
}