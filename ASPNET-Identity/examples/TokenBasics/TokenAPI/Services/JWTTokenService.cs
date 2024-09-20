using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TokenAPI.Models;

namespace TokenAPI.Services;

public class JWTTokenService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JWTTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Account user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, 
            SecurityAlgorithms.HmacSha256);

        // Create the JWT token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], 
            audience: _configuration["Jwt:Audience"], 
            claims: user.Claims, 
            expires: DateTime.Now.AddMinutes(
                _configuration.GetValue<int>("Jwt:Expiration")), 
            signingCredentials: credentials 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
