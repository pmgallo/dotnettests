using BlazorPlayground.Client.Data;
using BlazorPlayground.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorPlayground.Client.Pages;

public class PizzasComponentBase : ComponentBase
{
    [Inject]
    protected PizzaService pizzaService { get; set; }
    protected List<Pizza> todaysPizzas;
    
    protected List<PizzaSpecial> specials = new();

    protected override async Task OnInitializedAsync()
    {
        todaysPizzas = await pizzaService.GetPizzasAsync();
    }
    
    protected override void OnInitialized()
    {
        
    }
}