using System.Text.Json;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.DTOs;

namespace PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Services;

public class CalculateService : ICalculateService
{
    public async Task<GetAmountInCurrencyResponse> GetAmountInCurrency(GetAmountInCurrency data)
    {
        try
        {
            string targetCurrency = data.Currency.ToUpper();
            
            double usdRate = await GetRate("USD");
            double targetRate = await GetRate(targetCurrency);
            double amountInUsd = data.Amount / usdRate;
            double usdToTargetRate = usdRate/ targetRate;
            double amountInTarget = amountInUsd * usdToTargetRate;
 
            return new GetAmountInCurrencyResponse(
                Currency: targetCurrency,
                Amount: amountInTarget,
                Rate: data.Amount/amountInTarget,
                RateDate: DateTime.Now.ToString("yyyy-MM-dd")
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<Dictionary<string, double>> GetRates()
    {
        using var http = new HttpClient();
        string url = "https://api.nbp.pl/api/exchangerates/tables/A/?format=json";

        var response = await http.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        
        var rates = root[0].GetProperty("rates");

        var dictionary = new Dictionary<string, double>();

        foreach (var rate in rates.EnumerateArray())
        {
            string code = rate.GetProperty("code").GetString() ?? throw new InvalidOperationException();
            double mid = rate.GetProperty("mid").GetDouble();

            dictionary[code] = mid;
        }

        return dictionary;
    }

        
        
    private async Task<double> GetRate(string currency)
    {
        using var http = new HttpClient();
        string url = $"https://api.nbp.pl/api/exchangerates/rates/A/{currency}?format=json";

        var response = await http.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        return root
            .GetProperty("rates")[0]
            .GetProperty("mid")
            .GetDouble();
    }
}