using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YouPie.Core.Interfaces;
using YouPie.Core.Models;

namespace YouPie.Infrastructure;

public class LoginRepo : ILoginRepo
{
    private readonly IMongoCollection<User> _mongoCollection;

    public LoginRepo(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _mongoCollection = mongoDatabase.GetCollection<User>(nameof(User));
    }

    public async Task<User?> Login(UserDto userDto)
    {
        return await _mongoCollection.Find(x => x.Username == userDto.Username && x.Password == userDto.Password).FirstOrDefaultAsync();
    }

    public async Task Register(User newUser)
    {
        await _mongoCollection.InsertOneAsync(newUser);
    }

    public async Task ChangePassword(string email, string password, User updatedUser)
    {
        await _mongoCollection.ReplaceOneAsync(x => x.Email == email && x.Password == password, updatedUser);
    }
    
}