using System.Runtime.CompilerServices;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Data;

namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Services
{
    public class MonteCarloService : IMonteCarloService
    {
        public SimulationResult CalculatePi(long totalPoints)
        {
            int start = Environment.TickCount;

            int pointsInCircle = 0;
            Random random = new Random();

            for (int i = 0; i < totalPoints; i++)
            {
                double x = (random.NextDouble() * 2.0) - 1.0;
                double y = (random.NextDouble() * 2.0) - 1.0;

                if ((x * x + y * y) <= 1.0)
                {
                    pointsInCircle++;
                }
            }

            int stop = Environment.TickCount;

            double ratio = (double)pointsInCircle / totalPoints;

            return new SimulationResult
            {
                TotalPoints = totalPoints,
                PointsInCircle = pointsInCircle,
                EstimatedPi = 4.0 * ratio,
                Ratio = ratio,
                DurationMs = stop - start
            };
        }

        public async IAsyncEnumerable<BatchResult> StreamSimulationAsync(
            long totalPoints,
            int batchSize,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            if (totalPoints <= 0) throw new ArgumentException("Liczba punktów > 0");
            if (totalPoints > 1_000_000_000) throw new ArgumentException("Max 1 mld punktów");

            int start = Environment.TickCount;
            int pointsInCircle = 0;
            
            int visualizationSkip = 1;
            if (totalPoints > 100_000) visualizationSkip = 5;
            if (totalPoints > 1_000_000) visualizationSkip = 50;
            if (totalPoints > 10_000_000) visualizationSkip = 500;
            if (totalPoints > 100_000_000) visualizationSkip = 1500;
            
            int calculatedInterval = (int)(totalPoints / 200);
            int notificationInterval = Math.Max(batchSize, calculatedInterval);

            var currentBatch = new List<PointData>();
            var random = Random.Shared;

            for (int i = 1; i <= totalPoints; i++)
            {
                if (cancellationToken.IsCancellationRequested) yield break;
                
                double x = (random.NextDouble() * 2.0) - 1.0;
                double y = (random.NextDouble() * 2.0) - 1.0;
                bool isInside = (x * x + y * y) <= 1.0;

                if (isInside) pointsInCircle++;
                
                if (i % visualizationSkip == 0)
                {
                    currentBatch.Add(new PointData { X = x, Y = y, IsInside = isInside });
                }
                
                if (i % notificationInterval == 0 || i == totalPoints)
                {
                    int currentTick = Environment.TickCount;

                    var result = new BatchResult
                    {
                        Points = new List<PointData>(currentBatch),
                        TotalPointsProcessed = i,
                        PointsInCircle = pointsInCircle,
                        EstimatedPi = 4.0 * ((double)pointsInCircle / i),
                        Duration = currentTick - start
                    };

                    currentBatch.Clear();

                    if (totalPoints < 10000 && i < totalPoints)
                    {
                        await Task.Delay(1, cancellationToken);
                    }
                   
                    else if (i % 500_000 == 0)
                    {
                        await Task.Yield();
                    }

                    yield return result;
                }
            }
        }
    }
}