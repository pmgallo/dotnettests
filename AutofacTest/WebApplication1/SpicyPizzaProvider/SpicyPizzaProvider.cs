using Autofac;
using Common;

namespace SpicyPizzaProvider;

[DiscoverableForRegistration(DiscoveryRegistrationMode.ImplementedInterfaces, true)]
public class SpicyPizzaProvider : IPizzaProvider
{
    public Pizza GetPizza()
    {
        return new Pizza()
        {
            Name = "Spicy Pizza",
            IsGlutenFree = false,
            Size = 32
        };
    }
}