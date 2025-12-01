using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.DTOs;
using PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Services;

namespace PodstawyProgramowaniaObiektowego.Modules.CurrencyCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculateService _calculateService;

        public CalculatorController(ICalculateService calculateService)
        {
            _calculateService = calculateService;
        }
        
        [HttpGet("convert")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAmountInCurrency([FromQuery] GetAmountInCurrency data)
        {
            var response = await _calculateService.GetAmountInCurrency(data);

            if (response is null)
                return BadRequest("Nie udało się pobrać danych z NBP.");

            return Ok(response);
        }

        [HttpGet("rates")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRates()
        {
            var response = await _calculateService.GetRates();

            if (response is null)
                return BadRequest("Nie udało się pobrać danych z NBP.");
            
            return Ok(response);
        }
        
    }
}