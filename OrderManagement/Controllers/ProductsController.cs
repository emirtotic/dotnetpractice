using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTOs;
using OrderManagement.Services;

namespace OrderManagement.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{

    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAllAsync()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductResponse>> GetByIdAsync(long id)
    {
        var product = await _productService.GetByIdAsync(id);   

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateAsync(
        [FromBody] CreateProductRequest request)
    {
        var createdProduct = await _productService.CreateAsync(request);

        return CreatedAtAction(
            "GetById",
            new { id = createdProduct.ProductId },
            createdProduct
        );
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ProductResponse>> UpdateAsync(
        long id,
        [FromBody] UpdateProductRequest request)
    {
        var updatedProduct = await _productService.UpdateAsync(id, request);

        if (updatedProduct is null)
        {
            return NotFound();
        }

        return Ok(updatedProduct);
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }

    
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new InvalidOperationException("Testing exception to see does it work :)");
    }
}