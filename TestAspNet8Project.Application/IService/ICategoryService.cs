using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspNet8Project.Application.DTOs;
using TestAspNet8Project.Domain.Models;

namespace TestAspNet8Project.Application.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryRecords>> GetCategoriesAsync();
    }
}
