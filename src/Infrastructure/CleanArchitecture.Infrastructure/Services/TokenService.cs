﻿using CleanArchitecture.Application.DTOs.Tokens;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CleanArchitecture.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public TokenDTO CreateAccessToken(User user, IList<string> userRoles)
        {
            var accessTokenExpiration = DateTime.Now.AddDays(1);
            var refreshTokenExpiration = DateTime.Now.AddYears(1);
            var securityKey = SignService.GetSymmetricSecurityKey("");
            var audiences = new List<string>() { "audience" };

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken securityToken = new(
                issuer: "issuer",
                audience: "audience",
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                signingCredentials: signingCredentials,
                claims: GetClaims(user, userRoles, audiences)
                );

            JwtSecurityTokenHandler securityTokenHandler = new();

            return new TokenDTO()
            {
                AccessToken = securityTokenHandler.WriteToken(securityToken),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpiration = refreshTokenExpiration,
            };

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        private static IEnumerable<Claim> GetClaims(User user, IList<string> userRoles, List<string> audiences)
        {
            var userList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            userList.AddRange(userRoles.Select(x => new Claim("role", x)));
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }
    }
}