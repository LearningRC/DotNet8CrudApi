using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8CrudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<string> Products = new() { "Sugar", "Salt", "Rice" };

    [HttpGet]
    public IActionResult GetAll() => Ok(Products);

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        id >= 0 && id < Products.Count ? Ok(Products[id]) : NotFound();

    [HttpPost]
    public IActionResult Add([FromBody] string product)
    {
        Products.Add(product);
        return CreatedAtAction(nameof(Get), new { id = Products.Count - 1 }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] string updatedProduct)
    {
        if (id < 0 || id >= Products.Count) return NotFound();
        Products[id] = updatedProduct;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= Products.Count) return NotFound();
        Products.RemoveAt(id);
        return NoContent();
    }
}