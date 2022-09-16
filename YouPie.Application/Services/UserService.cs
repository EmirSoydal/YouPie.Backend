using Microsoft.Extensions.Logging;
using YouPie.Application.Interfaces;
using YouPie.Core.Interfaces;
using YouPie.Core.Models;

namespace YouPie.Application.Services;

public class UserService : IUserService
{
    private readonly ILogger _logger;
    private readonly IRepository<User> _repository;

    public UserService(ILogger logger, IRepository<User> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<User>> Get()
    {
        _logger.LogInformation("All users have been brought {Time}", DateTime.Now);
        return await _repository.GetAllAsync();
    }

    public async Task<User?> Get(string id)
    {
        var user = await _repository.GetAsync(id);
        if (user == null)
            _logger.LogWarning("User has not been found {Time}", DateTime.Now);
        _logger.LogInformation("User has been brought {Time}", DateTime.Now);
        return user;
    }

    public async Task Post(User newUser)
    {
        await _repository.CreateAsync(newUser);
        _logger.LogInformation("New User has been created {Time}",DateTime.Now);
        
    }

    public async Task Post(IEnumerable<User> newUsers)
    {
        await _repository.CreateManyAsync(newUsers);
        _logger.LogInformation("New Users have been created {Time}",DateTime.Now);
    }

    public async Task<bool> Put(string id, User updatedUser)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
        {
            _logger.LogWarning("User could not been updated, no user found {Time}",DateTime.Now);
            return false;
        }

        updatedUser.Id = user.Id;
        await _repository.ReplaceAsync(id, updatedUser);
        _logger.LogInformation("User has been updated {Time}",DateTime.Now);
        return true;
    }

    public async Task Delete()
    {
        await _repository.DeleteAllAsync();
        _logger.LogInformation("All users have been deleted {Time}", DateTime.Now);
    }

    public async Task<bool> Delete(string id)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
        {
            _logger.LogWarning("User could not been deleted, no user found {Time}",DateTime.Now);
            return false;
        }

        await _repository.DeleteAsync(id);
        _logger.LogInformation("User has been deleted {Time}", DateTime.Now);
        return true;
    }
}