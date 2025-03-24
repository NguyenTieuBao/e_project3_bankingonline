using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Data;
using BankingSystem.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
namespace BankingSystem.Services
{
    public class TokenService
    {
        private readonly EBankingonlineContext _context;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration, EBankingonlineContext context) 
        {
            _configuration = configuration;
            _context = context;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        }

        public string CreateTokenUser(User user, Userprofile userprofile, Adminrole adminrole)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (userprofile == null)
            {
                throw new ArgumentNullException(nameof(userprofile));
            }
            if (adminrole == null)
            {
                throw new ArgumentNullException(nameof(adminrole));
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, userprofile.Email),
                new Claim(JwtRegisteredClaimNames.Name, $"{userprofile.LastName + userprofile.FirstName}"),
                new Claim(ClaimTypes.Role, adminrole.RoleName)                
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateTokenAdmin(Admin admin, Adminprofile adminprofile, Adminrole adminrole)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, admin.Username),
                new Claim(JwtRegisteredClaimNames.Email, adminprofile.Email),
                new Claim(JwtRegisteredClaimNames.Name, $"{adminprofile.LastName + adminprofile.FirstName}"),
                new Claim(ClaimTypes.Role, adminrole.RoleName)                
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            var tokenInUser =  _context.Users.Any(x => x.RefreshToken == refreshToken);
            if(tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var tokenValidationParameters = new TokenValidationParameters{
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token,tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;
        }
    }

}