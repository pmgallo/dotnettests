using Autofac;
using Common;

namespace StandardPizzaProvider;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        Console.Out.WriteLine("Loading StandardPizzaProvider");
        builder.RegisterType<StandardPizzaProvider>().As<IPizzaProvider>();
    }
}