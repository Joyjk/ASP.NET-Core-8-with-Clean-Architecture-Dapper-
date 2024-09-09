using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestAspNet8Project.Application.IService;
using TestAspNet8Project.Application.Service;
using TestAspNet8Project.Domain.Interface;
using TestAspNet8Project.Domain.Models;



namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddTransient<IRedisCacheRepository, RedisCacheService>();
            return services;
        }

        
    }
}
