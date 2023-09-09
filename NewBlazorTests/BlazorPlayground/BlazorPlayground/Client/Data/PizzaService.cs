using BlazorPlayground.Client.State;
using BlazorPlayground.Shared;

namespace BlazorPlayground.Client.Data;

public class PizzaService
{
    private readonly PizzaSalesState _state;

    public PizzaService(PizzaSalesState state)
    {
        _state = state;
    }
    public async Task<List<Pizza>> GetPizzasAsync()
    {
        return new List<Pizza>
        {
            new Pizza { Name = "The Baconatorizor", Vegetarian = true, Description = "It has EVERY kind of bacon", Vegan = true, Price = 200},
            new Pizza { Name = "Buffalo chicken", Vegetarian = true, Description = "Spicy chicken, hot sauce, and blue cheese, guaranteed to warm you up", Vegan = true, Price = 200},
            new Pizza { Name = "Veggie Delight", Vegetarian = true, Description = "It's like salad, but on a pizza", Vegan = true, Price = 200},
            new Pizza { Name = "Margherita", Vegetarian = true, Description = "Traditional Italian pizza with tomatoes and basil", Vegan = true, Price = 200},
            new Pizza { Name = "Basic Cheese Pizza", Vegetarian = true, Description = "It's cheesy and delicious. Why wouldn't you want one?", Vegan = true, Price = 200},
            new Pizza { Name = "Classic pepperoni", Vegetarian = true, Description = "It's the pizza you grew up with, but Blazing hot!", Vegan = true , Price = 200}               
        };
    }
}