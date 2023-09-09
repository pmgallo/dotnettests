using BenchmarkDotNet.Attributes;

namespace Playground666;

[MemoryDiagnoser(false)]
public class AsyncTests
{
    private static readonly Task<int> DoSomethingTask =  Task.FromResult(1);

    private static Task<int> DoSomethingWithErrorTask() 
    {
        throw new Exception("This was intentionally thrown");
    }
    
    [Benchmark]
    public async Task<int> GetNumberAsync()
    {
        return await Task_AwaitCore();
    }
    
    [Benchmark]
    public async Task<int> GetNumber()      
    {
        return await Task_NoAwaitCore();
    }
    
    public async Task<int> GetNumberWithErrorAsync()
    {
        return await DoSomethingWithErrorTask();
    }
        
    public Task<int> GetNumbeWithError()
    {
        return DoSomethingWithErrorTask();
    }

    private async Task<int> Task_AwaitCore()
    {
        return await DoSomethingTask;
    }

    private Task<int> Task_NoAwaitCore()
    {
        return DoSomethingTask;
    }
}