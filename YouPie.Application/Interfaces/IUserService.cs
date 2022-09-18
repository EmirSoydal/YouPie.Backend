using Microsoft.AspNetCore.Mvc;
using YouPie.Core.Models;

namespace YouPie.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> Get();
    Task<User?> Get(string id);
    Task Post(User newUser);
    Task Post(IEnumerable<User> newUsers);
    Task<bool> Put(string id, User updatedUser);
    Task Delete();
    Task<bool> Delete(string id);
}