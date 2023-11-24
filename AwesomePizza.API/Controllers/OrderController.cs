using AwesomePizza.Ports.Output;
using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{

    [HttpPost]
    public Order Create()
    {
        return new Order($"{new Core.Order(new FakeRepository()).New()}");
    }
}

public record Order(string Id);

public class FakeRepository : IRepository<OrderId>
{
    public OrderId Save(string id)
    {
        return id;
    }
}