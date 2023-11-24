using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;
using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(IOrder order) : ControllerBase
{
    private readonly IOrder order = order;

    [HttpPost]
    public Order Create()
    {
        return new Order($"{this.order.New()}");
    }
}

public record Order(string Id);