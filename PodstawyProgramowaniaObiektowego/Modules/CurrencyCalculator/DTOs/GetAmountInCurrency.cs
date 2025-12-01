namespace PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.DTOs;

public record GetAmountInCurrency(
    string Currency, 
    double Amount
);