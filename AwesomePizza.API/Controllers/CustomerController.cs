using AwesomePizza.API.Models;
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
    public Ports.Order Get(string id)
    {
        return adapter.Get(id);
    }
}