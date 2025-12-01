using Microsoft.AspNetCore.SignalR;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Services;

namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Hubs
{
    public class MonteCarloHub : Hub
    {
        private readonly IMonteCarloService _monteCarloService;

        public MonteCarloHub(IMonteCarloService monteCarloService)
        {
            _monteCarloService = monteCarloService;
        }

        public async Task StartSimulation(int totalPoints)
        {
            var cancellationToken = Context.ConnectionAborted;

            try
            {
                await foreach (var batch in _monteCarloService.StreamSimulationAsync(totalPoints, 500, cancellationToken))
                {
                    await Clients.Caller.SendAsync("ReceiveBatch", batch, cancellationToken);
                }

                await Clients.Caller.SendAsync("SimulationFinished", cancellationToken);
            }
            catch (ArgumentException ex)
            {
                throw new HubException(ex.Message);
            }
            catch (Exception)
            {
                throw new HubException("Wystąpił nieoczekiwany błąd serwera podczas symulacji.");
            }
        }
    }
}