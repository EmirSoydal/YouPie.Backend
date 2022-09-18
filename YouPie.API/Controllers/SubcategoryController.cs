using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouPie.Application.Interfaces;
using YouPie.Core.Models;

namespace YouPie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _service;
    
    public SubcategoryController(ISubcategoryService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IEnumerable<Subcategory>> Get()
    {
        return await _service.Get();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Subcategory>> Get(string id)
    {
        var localSubcategory = await _service.Get(id);

        if (localSubcategory is null)
        {
            return NotFound();
        }

        return Ok(localSubcategory);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(Subcategory newSubcategory)
    {
        await _service.Post(newSubcategory);
        
        return CreatedAtAction(nameof(Get), new { id = newSubcategory.Id }, newSubcategory);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<Subcategory> newSubcategories)
    {
        await _service.Post(newSubcategories);

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Put(string id, Subcategory updatedSubcategory)
    {
        var found = await _service.Put(id, updatedSubcategory);
        
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
