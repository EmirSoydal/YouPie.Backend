using Microsoft.AspNetCore.Mvc;
using YouPie.Application.Interfaces;
using YouPie.Core.Models;


namespace YouPie.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    [HttpPost]
    public Task<IActionResult> Login(UserDto userData)
    {
        var login = _tokenService.Login(userData);
        
        return login.Result ==  null ? Task.FromResult<IActionResult>(BadRequest()) : Task.FromResult<IActionResult>(Ok(login));
    }

    [HttpPost]
    public async Task<IActionResult> Register(User userData)
    {
        var registered = await _tokenService.Register(userData);
        
        if (registered is false)
            return BadRequest();
        
        return Ok("Registered.");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string id, string newPassword)
    {
        var found = await _tokenService.ChangePassword(id, newPassword);
        
        if (found is false)
            return BadRequest();
        
        return Ok("Successfully changed.");
    }
}