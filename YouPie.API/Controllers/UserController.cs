using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouPie.Application.Interfaces;
using YouPie.Core.Models;

namespace YouPie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _service.Get();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _service.Get(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _service.Post(newUser);
        
        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<User> newUsers)
    {
        await _service.Post(newUsers);

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Put(string id, User updatedUser)
    {
        var found = await _service.Put(id, updatedUser);
        
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
