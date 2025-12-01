namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Data;

public class BatchResult
{
    public List<PointData> Points { get; set; } = new List<PointData>();
    public int TotalPointsProcessed { get; set; }
    public int PointsInCircle { get; set; }
    public double EstimatedPi { get; set; }
    public long Duration { get; set; }
}