using BenchmarkDotNet.Attributes;

namespace Playground666;

[MemoryDiagnoser(false)]
public class SpanTests
{
    private Person[] persons;

    [Benchmark]
    public void IterateArray()
    {
        foreach (Person p in persons)
        {
            
        }
    }
    
    [Benchmark]
    public void IterateSpan()
    {
        foreach (Person p in persons.AsSpan())
        {
            
        }
    }

    [GlobalSetup]
    public async Task Setup()
    {
        List<Person> persons = new List<Person>();
        for (int i = 1; i <= 1000000; i++)
        {
            persons.Add(new Person(){Name = Guid.NewGuid().ToString(), Bike = new Bike(){ model = "pepe", gearsCount = 434}});
        }

        this.persons = persons.ToArray();
    }
}