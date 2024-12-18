using CatalogService.Application.DTOs;
using CatalogService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductGetDto>>> GetAll() 
    {
        var products = await _service.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductGetDto>> GetById(Guid id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null) 
            return NotFound();
        
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductCreateDto dto)
    {
        var addedProduct = await _service.AddProductAsync(dto);
        if (addedProduct == null)
            return BadRequest("Product could not be added");
        
        return CreatedAtAction(nameof(GetById), new { id = addedProduct.Id }, addedProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductUpdateDto dto)
    {
        var isUpdated = await _service.UpdateProductAsync(id, dto);
        if (!isUpdated) 
            return NotFound();
         
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isDeleted = await _service.DeleteProductAsync(id);
        if (!isDeleted) return NotFound();
        return NoContent();
    }
}