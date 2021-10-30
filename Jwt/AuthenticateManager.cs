using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jwt
{
    public class AuthenticateManager : IJwtAuthenticateManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>{
            {"deneme","132" }
            ,{"test","123"}
            ,{"admin","admin"}
            };
        private string _key;
        public AuthenticateManager(string key)
        {
            _key = key;
        }
        public string Authenticate(string userName, string password)
        {
            if (!users.Any(_=>_.Key==userName && _.Value==password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName)

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials=new SigningCredentials(
                                            new SymmetricSecurityKey(tokenKey),
                                            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
