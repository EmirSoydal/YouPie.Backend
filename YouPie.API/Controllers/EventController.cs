using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouPie.Application.Interfaces;
using YouPie.Core.Models;

namespace YouPie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;
    
    public EventController(IEventService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IEnumerable<Event>> Get()
    {
        return await _service.Get();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> Get(string id)
    {
        var localEvent = await _service.Get(id);

        if (localEvent is null)
        {
            return NotFound();
        }

        return Ok(localEvent);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(Event newEvent)
    {
        await _service.Post(newEvent);
        
        return CreatedAtAction(nameof(Get), new { id = newEvent.Id }, newEvent);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<Event> newEvents)
    {
        await _service.Post(newEvents);

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Put(string id, Event updatedEvent)
    {
        var found = await _service.Put(id, updatedEvent);
        
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
