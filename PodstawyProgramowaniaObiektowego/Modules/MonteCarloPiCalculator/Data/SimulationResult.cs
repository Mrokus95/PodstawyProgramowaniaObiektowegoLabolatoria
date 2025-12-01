namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Data
{
    public class SimulationResult
    {
        public long TotalPoints { get; set; }
        public long PointsInCircle { get; set; }
        public double EstimatedPi { get; set; }
        public double DurationMs { get; set; }
        public double Ratio { get; set; }
        
        public double CalculateAbsoluteError()
        {
            return System.Math.Abs(System.Math.PI - EstimatedPi);
        }
    }
}