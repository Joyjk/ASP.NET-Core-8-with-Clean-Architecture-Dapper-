using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspNet8Project.Application.IService;
using TestAspNet8Project.Application.Service;
using TestAspNet8Project.Domain.Interface;
using TestAspNet8Project.Domain.Models;
using TestAspNet8Project.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Data;
using TestAspNet8Project.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILoginRepository, LoginRepository>();


        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

        string connectionString = configuration["ConnectionStrings:DefaultConnection"]??string.Empty;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"] ?? ""))
                };

            });

        services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString.ToString()));

        services.AddScoped<IDapperRepository, DapperRepository>();


        return services;
    }
}

