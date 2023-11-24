using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(OrderAdapter adapter) : ControllerBase
{

    [HttpPost]
    public Order Create()
    {
        return adapter.Create();
    }
}