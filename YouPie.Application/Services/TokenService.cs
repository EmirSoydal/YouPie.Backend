using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using YouPie.Application.Interfaces;
using YouPie.Core.Interfaces;
using YouPie.Core.Models;
using YouPie.Infrastructure;

namespace YouPie.Application.Services;

public class TokenService : ITokenService
{
    private readonly ILoginRepo _repository;
    private readonly IRepository<User> _userRepo;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public TokenService(ILoginRepo repository, IConfiguration configuration, ILogger logger, IRepository<User> userRepo)
    {
        _repository = repository;
        _configuration = configuration;
        _logger = logger;
        _userRepo = userRepo;
    }

    public async Task<object?> Login(UserDto userData)
    {
        if (userData.Username == null || userData.Password == null) return null;
        var user = await _repository.Login(userData);
        if (user == null) return null;
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim("UserId", user.Id!),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> Register(User userData)
    {
        await _repository.Register(userData);
        _logger.LogInformation("User has been created {Time}", DateTime.Now);
        return true;
    }

    public async Task<bool> ChangePassword(string id, string newPassword)
    {
        var user = await _userRepo.GetAsync(id);
        if (user is null)
        {
            _logger.LogWarning("User could not been found, no password change {Time}", DateTime.Now);
            return false;
        }

        user.Password = newPassword;
        await _repository.ChangePassword(id, user);
        _logger.LogInformation("User's password has been updated {Time} {User}",DateTime.Now,user.Id);
        return true;
    }
}