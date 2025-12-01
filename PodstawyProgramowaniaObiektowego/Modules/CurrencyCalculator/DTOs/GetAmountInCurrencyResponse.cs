namespace PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.DTOs;

public record GetAmountInCurrencyResponse(
    string Currency,
    double Amount,
    double Rate,
    string RateDate
);