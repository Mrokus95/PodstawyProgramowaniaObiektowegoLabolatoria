namespace PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Services
{
    public interface IFibonacciService
    {
        IEnumerable<long> GetFibonacciSequence(int count);
    }
}