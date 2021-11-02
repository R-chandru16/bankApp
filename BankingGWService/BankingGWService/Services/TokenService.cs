using BankingGWService.Models;
using BankingGWService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingGWService.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string CreateToken(UserDTO userDTO)
        {
            //claim
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userDTO.Email),
                new Claim("role", userDTO.Role),
            };
            //Credentials
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //Token Description
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1000),
                SigningCredentials = cred
            };
            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
        public string CreateEmailToken(UserDTO userDTO)
        {
            //claim
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userDTO.Email),
                new Claim("role", userDTO.Role),
            };
            //Credentials
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //Token Description
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cred
            };
            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}