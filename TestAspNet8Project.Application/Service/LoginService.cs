using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TestAspNet8Project.Domain.Models;
using Newtonsoft.Json;
using TestAspNet8Project.Application.IService;

namespace TestAspNet8Project.Application.Service
{
    
    public class LoginService : ILoginService//Repository
    {
        private readonly JWTSettings _jwtSettings;
        public LoginService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<JwtSecurityToken> GenerateJWToken(string user_id, string password)
        {
            var user = new
            {
                user_id,
                password
            };
            var claims = new[]
            {
                 new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),

                 new Claim("uid", user_id),
                 new Claim("user_name", password)
             };
            return JWTGeneration(claims);

        }
        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
