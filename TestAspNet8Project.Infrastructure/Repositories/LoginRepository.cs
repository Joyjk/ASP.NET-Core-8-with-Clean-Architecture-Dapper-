using System.IdentityModel.Tokens.Jwt;
using TestAspNet8Project.Application.IService;
using TestAspNet8Project.Domain.Interface;
using TestAspNet8Project.Domain.Models;

namespace TestAspNet8Project.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ILoginService _loginService;
        public LoginRepository(ILoginService loginRepository)
        {
            _loginService = loginRepository;
        }
        public async Task<object> Login(string username, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var response = new TokenResponse();

                    var jwtSecurityToken = await _loginService.GenerateJWToken(username, password);

                    response.UserName = username;
                    response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    var expClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
                    if (expClaim != null && long.TryParse(expClaim, out long expUnix))
                    {
                        var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).DateTime;
                        response.ExpiresOn = expirationDateTime;
                    }
                    //response.ExpiresOn = DateTime.Now.AddYears(1);
                    response.IssuedOn = DateTime.Now;

                    return response;
                }
                else
                {
                    return new { Error = "Invalid username or password" };
                }

            }
            catch (Exception ex) {
             throw new Exception(ex.Message);
            }
            
        }
    }
}
