using ExamManagement.API.Data;
using ExamManagement.API.DTOs;
using ExamManagement.API.Models;

namespace ExamManagement.API.Services;

public class DbService : IDbService
{
    private readonly UserDb _userDb;

    public DbService(UserDb userDb)
    {
        _userDb = userDb;
    }

    public async Task<User> CreateUserAsync(
        UserDto request, byte[] passwordHash, byte[] passwordSalt)
    {
        var name = request.Name;
        var email = request.Email;
        var role = request.Role;
        var user = new User(
            name, email, passwordHash, passwordSalt, role);
        await _userDb.CreateUserAsync(user);
        return user;
    }

    public async Task<User> GetUserAsync(string email)
    {
        return await _userDb.GetUserAsync(email);
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _userDb.GetUsersAsync();
    }
}

public interface IDbService
{
    Task<List<User>> GetUsersAsync();
    Task<User> CreateUserAsync(UserDto request, byte[] passwordHash, byte[] passwordSalt);
    Task<User> GetUserAsync(string email);
}
