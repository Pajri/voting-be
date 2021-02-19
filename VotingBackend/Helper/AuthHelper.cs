using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VotingBackend.Models;

namespace VotingBackend.Helper
{
    public interface IAuthHelper
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        public string GenerateToken(User user, string secret, TokenType tokenType);
    }

    public enum TokenType
    {
        ACCESS_TOKEN, REFRESH_TOKEN
    };

    public class AuthHelper : IAuthHelper
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string GenerateToken(User user, string secret, TokenType tokenType)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[] { 
                new Claim(ClaimTypes.Name, user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var exp = new DateTime();
            if(tokenType == TokenType.ACCESS_TOKEN)
            {
                exp = DateTime.UtcNow.AddMinutes(15);
            }
            else
            {
                exp = DateTime.UtcNow.AddHours(1);
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = exp,
                SigningCredentials = creds
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
