using YouPie.Core.Models;

namespace YouPie.Application.Interfaces;

public interface ITokenService
{
    Task<object?> Login(UserDto userData);
}