using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Data;

namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Services
{
    public interface IMonteCarloService
    {
        SimulationResult CalculatePi(long totalPoints);

        IAsyncEnumerable<BatchResult> StreamSimulationAsync(
            long totalPoints,
            int batchSize,
            CancellationToken cancellationToken);
    }
}