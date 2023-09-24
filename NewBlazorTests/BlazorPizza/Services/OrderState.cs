namespace BlazingPizza.Services;

public class OrderState
{
    public bool ShowingConfigureDialog { get; private set; }
    public Pizza ConfiguringPizza { get; private set; }
    public Order Order { get; private set; } = new Order();

    private static int _orderId = 0;

    public OrderState()
    {
        
    }

    public static int GetNextOrderId()
    {
        return ++_orderId;
    }


    private int _pizzaCounter = 0;
    public void ShowConfigurePizzaDialog(PizzaSpecial special)
    {
        if (_pizzaCounter++ > 3)
            throw new InvalidOperationException("Too many pizzas selected");
        
        ConfiguringPizza = new Pizza()
        {
            Special = special,
            SpecialId = special.Id,
            Size = Pizza.DefaultSize,
            Toppings = new List<PizzaTopping>(),
        };

        ShowingConfigureDialog = true;
    }

    public void CancelConfigurePizzaDialog()
    {
        ConfiguringPizza = null;

        ShowingConfigureDialog = false;
    }

    public void ConfirmConfigurePizzaDialog()
    {
        Order.Pizzas.Add(ConfiguringPizza);
        ConfiguringPizza = null;

        ShowingConfigureDialog = false;
    }
    
    public void RemoveConfiguredPizza(Pizza pizza)
    {
        Order.Pizzas.Remove(pizza);
    }
    
    public void ResetOrder()
    {
        Order = new Order();
    }
}