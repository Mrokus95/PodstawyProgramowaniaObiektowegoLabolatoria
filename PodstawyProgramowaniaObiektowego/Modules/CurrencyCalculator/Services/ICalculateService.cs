using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.DTOs;

namespace PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Services
{
    public interface ICalculateService
    {
        Task<GetAmountInCurrencyResponse> GetAmountInCurrency(GetAmountInCurrency data);
        Task<Dictionary<string, double>> GetRates();
    }
}