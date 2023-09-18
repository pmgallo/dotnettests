using Autofac;
using Common;

namespace SpicyPizzaProvider;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        Console.Out.WriteLine("Loading StandardPizzaProvider");
        builder.RegisterType<SpicyPizzaProvider>().As<IPizzaProvider>();
    }
}