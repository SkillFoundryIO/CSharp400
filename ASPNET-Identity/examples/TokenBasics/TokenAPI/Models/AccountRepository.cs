﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TokenAPI.Models;

public static class AccountRepository
{
    private static List<Account> _accounts;

    static AccountRepository()
    {
        _accounts =
        [
            new Account
            {
                UserName = "admin",
                Password = "admin123",
                Claims = new List<Claim>
                {
                    new Claim("admin", "true"),
                    new Claim(JwtRegisteredClaimNames.Name, "admin")
                }
            },
            new Account
            {
                UserName = "john.doe",
                Password = "user123",
                Claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, "john.doe"),
                    new Claim("DateOfBirth", "03/15/2006"),
                    new Claim(JwtRegisteredClaimNames.Email, "john.doe@example.com")
                }
            },
        ];
    }

    public static Account Login(string username, string password)
    {
        return _accounts.FirstOrDefault(a => a.UserName == username && a.Password == password);
    }
}

public class Account
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<Claim> Claims { get; set; }
}
