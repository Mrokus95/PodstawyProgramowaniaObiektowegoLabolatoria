using Microsoft.Extensions.Caching.Memory;

namespace PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Services;

public class FibonacciService : IFibonacciService
{
    private readonly IMemoryCache _cache;

    public FibonacciService(IMemoryCache cache)
    {
        _cache = cache;
    }

    private long Fibonacci(int n)
    {
        return _cache.GetOrCreate(n, entry =>
        {
            if (n <= 1) return n;
            
            entry.SetSlidingExpiration(TimeSpan.FromHours(1));
            entry.Priority = CacheItemPriority.High;

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        });
    }

    public IEnumerable<long> GetFibonacciSequence(int count)
    {
        for (int i = 0; i < count; i++)
            yield return Fibonacci(i);
    }
}