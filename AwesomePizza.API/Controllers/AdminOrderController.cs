using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController()]
[Route("api/admin")]
public class AdminController(OrderAdapter adapter) : ControllerBase
{
    [HttpGet("order")]
    public IEnumerable<Order> List()
    {
        return adapter.List();
    }
}
