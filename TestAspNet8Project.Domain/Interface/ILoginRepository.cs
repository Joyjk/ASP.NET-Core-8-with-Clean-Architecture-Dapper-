using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAspNet8Project.Domain.Interface
{
    public interface ILoginRepository
    {
        Task<object> Login(string username, string password);
    }
}
