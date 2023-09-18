namespace Common;

public class PizzaProvidersContainer : IPizzaProvider
{
    private readonly IReadOnlyCollection<IPizzaProvider> _repositories;

    public PizzaProvidersContainer(IEnumerable<IPizzaProvider> repositories)
    {
        _repositories = repositories.ToArray();
    }

    public IEnumerable<Pizza> GetAllObjects()
        => _repositories.SelectMany(x => new List<Pizza>(){x.GetPizza()});
    
    public Pizza GetPizza()
    {
        return _repositories.FirstOrDefault()?.GetPizza();
    }
}