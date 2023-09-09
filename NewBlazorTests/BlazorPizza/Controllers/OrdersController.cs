using BlazingPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza;

[Route("orders")]
[ApiController]
public class OrdersController : Controller
{
    private static List<Order> _orders = new List<Order>();

    public OrdersController()
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
    {
        return _orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
    }
    
    [HttpGet("{orderId}")]
    public ActionResult<OrderWithStatus> GetOrderWithStatus(int orderId)
    {
        var order = _orders
            .Where(o => o.OrderId == orderId)
            .SingleOrDefault();

        if (order == null)
        {
            return NotFound();
        }

        return OrderWithStatus.FromOrder(order);
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder(Order order)
    {
        order.CreatedTime = DateTime.Now;

        // Enforce existence of Pizza.SpecialId and Topping.ToppingId
        // in the database - prevent the submitter from making up
        // new specials and toppings
        foreach (var pizza in order.Pizzas)
        {
            pizza.SpecialId = pizza.Special.Id;
            //The property pizza.Special should be set to null if EF is used
            //pizza.Special = null;
        }

        order.OrderId = OrderState.GetNextOrderId();
        _orders.Add(order);
        return order.OrderId;
    }
}