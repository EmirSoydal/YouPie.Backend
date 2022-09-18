using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouPie.Application.Interfaces;
using YouPie.Core.Models;

namespace YouPie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
    
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IEnumerable<Category>> Get()
    {
        return await _service.Get();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> Get(string id)
    {
        var localCategory = await _service.Get(id);

        if (localCategory is null)
        {
            return NotFound();
        }

        return Ok(localCategory);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(Category newCategory)
    {
        await _service.Post(newCategory);
        
        return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<Category> newCategories)
    {
        await _service.Post(newCategories);

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Put(string id, Category updatedCategory)
    {
        var found = await _service.Put(id, updatedCategory);
        
        if (found == false)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        await _service.Delete();
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var found = await _service.Delete(id);
        if (found == false)
        {
            return NotFound();
        }

        return NoContent();
    }
    }
}
