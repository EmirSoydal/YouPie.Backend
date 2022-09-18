using YouPie.Core.Models;

namespace YouPie.Application.Interfaces;

public interface ITokenService
{
    Task<object?> Login(UserDto userData);
    Task<bool> Register(User userData);
    Task<bool> ChangePassword(string id, string newPassword);
}