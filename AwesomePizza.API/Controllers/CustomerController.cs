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
    public IActionResult Get(string id)
    {
        var result = adapter.Get(id);
        if(result is null)
        {
            return NotFound(new { message = "Order not found" });
        }
        return Ok(result);
    }
}