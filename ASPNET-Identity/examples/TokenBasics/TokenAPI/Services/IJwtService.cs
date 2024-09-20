using TokenAPI.Models;

namespace TokenAPI.Services;

public interface IJwtService
{
    string GenerateToken(Account user);
}
