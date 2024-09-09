using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspNet8Project.Domain.Models;

namespace TestAspNet8Project.Application.DTOs
{
    public record CategoryRecords
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CreateBy { get; set; } = string.Empty;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Category, CategoryRecords>().ReverseMap();
            }
        }
    }
    
}
