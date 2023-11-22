using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{

    [HttpPost]
    public Order Create()
    {
        return new Order("aaa");
    }
}


public record Order(string Id);