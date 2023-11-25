using AwesomePizza.Ports;
using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController]
[Route("api")]
public class CustomerController(OrderAdapter adapter) : ControllerBase
{

    [HttpPost("order")]
    public OrderId Create()
    {
        return adapter.Create();
    }

    [HttpGet("order/{id}")]
    public OrderDetails Get(string id)
    {
        return adapter.Get(id);
    }
}