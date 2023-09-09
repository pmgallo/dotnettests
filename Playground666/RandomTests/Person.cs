namespace RandomTests;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public async Task ThinkForAWhile()
    {
        await Task.Delay(5000);
    }
}