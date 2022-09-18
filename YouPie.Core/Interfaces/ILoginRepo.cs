using YouPie.Core.Models;

namespace YouPie.Core.Interfaces;

public interface ILoginRepo
{
    Task<User?> Login(UserDto userDto);
    Task Register(User newUser);
    Task ChangePassword(string email, User updatedUser);
}