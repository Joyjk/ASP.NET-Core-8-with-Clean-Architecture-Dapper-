using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAspNet8Project.Application.IService
{
    public interface ILoginService
    {
        Task<JwtSecurityToken> GenerateJWToken(string user_id, string password);
    }
}
