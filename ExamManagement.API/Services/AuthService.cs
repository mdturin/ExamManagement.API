using ExamManagement.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExamManagement.API.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    void IAuthService.CreatePasswordHash(
        string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        byte[] buffer = Encoding.UTF8.GetBytes(password);
        passwordHash = hmac.ComputeHash(buffer);
    }

    User IAuthService.CreateUser(
        UserDto request, byte[] passwordHash, byte[] passwordSalt)
    {
        var name = request.Name;
        var email = request.Email;
        var role = request.Role;
        var user = new User(name, email, passwordHash, passwordSalt, role);
        return user;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = _config.GetSection("AppSettings:Token").Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var userToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(userToken);
    }
}

public interface IAuthService
{
    void CreatePasswordHash(
        string password, out byte[] passwordHash, out byte[] passwordSalt);
    User CreateUser(UserDto request, byte[] passwordHash, byte[] passwordSalt);
    string GenerateJwtToken(User user);
}
