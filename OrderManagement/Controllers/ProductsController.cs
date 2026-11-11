using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Controllers;
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{

    [HttpGet(Name = "GetProducts")]
    public IActionResult GetProducts()
    {
        
        return Ok("Products API is working");
    }
}