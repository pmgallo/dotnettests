namespace BlazorPlayground.Client.State;

public class PizzaSalesState
{
    public PizzaSalesState()
    {
        PizzasSoldToday = 0;
    }
    public int PizzasSoldToday { get; set; }
}