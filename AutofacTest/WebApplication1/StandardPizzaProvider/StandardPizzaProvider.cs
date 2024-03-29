﻿using Autofac;
using Common;

namespace StandardPizzaProvider;

[DiscoverableForRegistration(DiscoveryRegistrationMode.ImplementedInterfaces, true)]
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