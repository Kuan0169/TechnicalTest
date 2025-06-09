using Microsoft.AspNetCore.Mvc;
using MyCompany.Test.Infrastructure.Models;
using MyCompany.Test.Infrastructure.Services;

namespace MyCompany.Test.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await productService.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    [Route("createProduct")]
    public async Task<IActionResult> Create(ProductModel productModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await productService.CreateAsync(productModel);
        return Ok(created);
    }

    [HttpPut]
    [Route("updateProduct/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, ProductModel productModel)
    {
        await productService.UpdateAsync(id,productModel);
        return Ok(productModel);
    }

    [HttpDelete]
    [Route("deleteProduct/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await productService.DeleteAsync(id);
        return Ok();
    }
}