using Microsoft.AspNetCore.Mvc;
using YouPie.Application.Interfaces;
using YouPie.Core.Models;


namespace YouPie.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public Task<IActionResult> Post(UserDto userData)
    {
        var login = _tokenService.Login(userData);
        return login.Result ==  null ? Task.FromResult<IActionResult>(BadRequest()) : Task.FromResult<IActionResult>(Ok(login));
    }
}