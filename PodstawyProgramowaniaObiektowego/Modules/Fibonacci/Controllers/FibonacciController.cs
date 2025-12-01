using Microsoft.AspNetCore.Mvc;
using PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Services;

namespace PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _fibonacciService;

        public FibonacciController(IFibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
        }

        [HttpGet("generate")]
        public IActionResult GetSequence([FromQuery] int count = 10)
        {
            if (count < 1 || count > 30)
            {
                return BadRequest("Podaj liczbę z zakresu 1-30.");
            }

            var result = _fibonacciService.GetFibonacciSequence(count);
            return Ok(result);
        }
    }
}