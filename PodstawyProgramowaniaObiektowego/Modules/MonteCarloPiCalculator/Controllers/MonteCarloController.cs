using Microsoft.AspNetCore.Mvc;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.DTOs;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Services;

namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonteCarloController : ControllerBase
    {
        private readonly IMonteCarloService _monteCarloService;

        public MonteCarloController(IMonteCarloService monteCarloService)
        {
            _monteCarloService = monteCarloService;
        }
        
        [HttpGet]
        public IActionResult Get([FromQuery] SimulationRequest request)
        {
            var result = _monteCarloService.CalculatePi(request.Points);
            return Ok(result);
        }
    }
}