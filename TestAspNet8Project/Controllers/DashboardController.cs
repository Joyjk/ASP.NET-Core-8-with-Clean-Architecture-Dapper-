using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAspNet8Project.Application.IService;

namespace TestAspNet8Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public DashboardController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("GetAllData")]
        public async Task<IActionResult> GetAllData()
        {
            var entity = await _categoryService.GetCategoriesAsync();
            return Ok(entity);
        }

    }
}
