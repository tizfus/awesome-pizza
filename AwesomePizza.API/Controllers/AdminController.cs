using AwesomePizza.API.Models;
using AwesomePizza.Ports;
using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController(OrderAdapter adapter) : ControllerBase
{
    [HttpGet("order")]
    public IEnumerable<Order> List()
    {
        return adapter.List();
    }

    [HttpPut("order/{id}")]
    public Order Update(string id, [FromBody] UpdateRequest payload)
    {
        return adapter.UpdateStatus(id, payload);
    }
}