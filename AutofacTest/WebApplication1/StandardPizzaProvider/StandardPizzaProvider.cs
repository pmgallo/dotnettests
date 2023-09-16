using Common;

namespace StandardPizzaProvider;

[DiscoverableRouteRegistrarAttribute]
public class StandardPizzaProvider : IPizzaProvider
{
    public Pizza GetPizza()
    {
        return new Pizza()
        {
            Name = "Standard Pizza",
            IsGlutenFree = false,
            Size = 32
        };
    }
}