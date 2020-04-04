using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Workshop.IntegrationTests.Platform.Models;

namespace Workshop.IntegrationTests.Platform.Jwt
{
    public static class JwtHelper
    {
        public static UserJwt GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email ?? string.Empty),
                new Claim("user.id", user.Id.ToString()),
            };

            var issueDate = DateTime.UtcNow;
            var expirationDate = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(
                JwtSettings.Issuer,
                JwtSettings.Audience,
                claims,
                expires: expirationDate,
                signingCredentials: JwtSettings.Credentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserJwt
            {
                Created = issueDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = encodedJwt,
                UserId = user.Id
            };
        }
    }
}
