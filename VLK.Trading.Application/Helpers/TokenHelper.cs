using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VLK.Trading.Domain.Models;

namespace VLK.Trading.Application.Helpers
{
    public static class TokenHelper
    {
        public static string CreateToken(User user, string signingKey, string issuer, string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] Claims =
                  [
                      new Claim("userId", user.Id.ToString()),
                      new Claim("sub", user.Email),
                      new Claim("name", user.UserName),
                      new Claim("aud", audience)
                    ];

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: Claims,
                    expires: DateTime.Now.AddHours(1),
                    notBefore: DateTime.Now,
                    signingCredentials: credentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
