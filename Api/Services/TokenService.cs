using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Common;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TokenService
{
    private readonly CarRentalDbContext _context;

    public TokenService(CarRentalDbContext context)
    {
        _context = context;
    }
     public string CreateToken(User user)
    {
        var roleNames = _context.Users
            .Where(u => u.Id == user.Id)
            .SelectMany(u => u.Roles)
            .Select(ur => ur.Role.RoleName)
            .ToList();

        var claims = new List<Claim>
        {
            new Claim("loggedUserId", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        if (roleNames.Any())
        {
            foreach (var rn in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, rn));
            }
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, "Customer"));
        }

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("!Password123!Password123!Password123"));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer:"radostin",
            audience:"university",
            claims:claims,
            expires:DateTime.Now.AddMinutes(10),
            signingCredentials:cred
        );

        string tokenData= new JwtSecurityTokenHandler()
                                .WriteToken(token);
        return tokenData;
    }
}
