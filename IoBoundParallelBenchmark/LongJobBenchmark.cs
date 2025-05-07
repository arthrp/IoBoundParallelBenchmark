using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;

namespace IoBoundParallelBenchmark;

[HtmlExporter]
[RPlotExporter]
public class LongJobBenchmark
{
    [Params(2,5,10)]
    public int Count { get; set; }

    [Benchmark]
    public async Task<List<bool>> DoAsync()
    {
        var x = new LongJobManager();
        var result = new List<bool>();
        var list = Enumerable.Range(1, Count);
        
        foreach (var _ in list)
        {
            var y = await x.DoStuff();
            result.Add(y);
        }

        return result;
    }

    [Benchmark]
    public List<bool> DoParallel()
    {
        var x = new LongJobManager();
        var bag = new ConcurrentBag<bool>();
        var list = Enumerable.Range(1, Count);

        Parallel.ForEach(list, (el) =>
        {
            var res = x.DoStuff().GetAwaiter().GetResult();
            bag.Add(res);
        });

        return bag.ToList();
    }

    [Benchmark]
    public async Task<List<bool>> DoParallelAsync()
    {
        var x = new LongJobManager();
        var bag = new ConcurrentBag<bool>();
        var list = Enumerable.Range(1, Count);

        await Parallel.ForEachAsync(list, async (i, token) =>
        {
            var y = await x.DoStuff();
            bag.Add(y);
        });

        return bag.ToList();
    }
}