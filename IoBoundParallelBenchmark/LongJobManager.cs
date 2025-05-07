namespace IoBoundParallelBenchmark;

public class LongJobManager
{
    public async Task<bool> DoStuff()
    {
        await Task.Delay(250);
        return true;
    }
}