using AwesomePizza.API.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

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
    public IActionResult Get(string id)
    {
        return adapter.Get(id) switch
        {
            { Succeeded: true, Value: var result } => Ok(result),
            _ => NotFound(new { message = "Order not found" })
        };
    }
}