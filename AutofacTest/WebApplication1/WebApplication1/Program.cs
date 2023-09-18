using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Module = Autofac.Module;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IPizzaProvider pizzaProvider) =>
    {
        var pizza = pizzaProvider.GetPizza();
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<StandardPizzaProvider.StandardPizzaProvider>().As<IPizzaProvider>();
        
        var assemblies = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins"), "*.dll").Select(Assembly.LoadFrom).ToList();
        
        //Escenario 1. Carga manualmente cada tipo que necuentra en los assemblies como instancia de IPizzaProvider. Este escenario se puede mejorar si en lugar de dejar explícito el tipo IPizzaProvider se usara AsImplementedInterfaces
        /* var discoverableRouteRegistrarTypes = 
            assemblies
            .SelectMany(x => x
                .GetTypes()
                .Where(x => x.CustomAttributes.Any(attr => attr.AttributeType == typeof(DiscoverableRouteRegistrarAttribute))))
            .ToArray();
        foreach (var type in discoverableRouteRegistrarTypes)
        {
            builder.RegisterType(type).As<IPizzaProvider>().SingleInstance();
        }
        */
        
        //Escenario 2: Escanea los assemblies y registra todos los tipos de datos asociados a sus respectivas interfaces. Mejora del escenario anterior
        /*
        assemblies.ForEach(assembly => builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith(""))
            .AsImplementedInterfaces());
            */
        
        /*Escenario 3: Escanea los assemblies y carga los módulos. Esto delega la tarea de carga de las dependencias a los plugins, que no se si es buena idea*/
        /* assemblies.ForEach(assembly => 
            builder.RegisterAssemblyModules(assembly)); */
            
        
        
        /*Escenario 4: Carga el objecto PizzaProvidersContainer que tiene una lista de las implementaciones de IPizzaProvider que carga de los plugins*/
        
        builder
            .RegisterFacadeWithDiscoverableSources<PizzaProvidersContainer, IPizzaProvider>()
            .AsImplementedInterfaces()
            .SingleInstance();
        
        foreach (var assembly in assemblies)
        {
            builder.RegisterDiscoverableTypes(assembly);
        }
        
    }
}