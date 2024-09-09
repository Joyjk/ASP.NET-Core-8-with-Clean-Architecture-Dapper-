using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAspNet8Project.Domain.Interface;
using TestAspNet8Project.Domain.Models;
using TestAspNet8Project.Infrastructure.Repositories;

namespace TestAspNet8Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel login)
        { 
            var data = await _loginRepository.Login(login.Username, login.Password);
            return Ok(data);
        }


    }
}
