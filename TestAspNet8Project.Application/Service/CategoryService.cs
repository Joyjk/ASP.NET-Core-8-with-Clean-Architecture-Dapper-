using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestAspNet8Project.Application.DTOs;
using TestAspNet8Project.Application.IService;
using TestAspNet8Project.Domain.Interface;
using TestAspNet8Project.Domain.Models;

namespace TestAspNet8Project.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IDapperRepository _repository;
        private readonly IRedisCacheRepository _redisCacheService;
        private const string categoryListCacheKey = "categoryList";
        private readonly IMapper _mapper;
       public CategoryService(IDapperRepository repository, IMapper mapper, IRedisCacheRepository redisCacheService)
        {
            _repository = repository;
            _redisCacheService = redisCacheService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryRecords>> GetCategoriesAsync()
        {
            try
            {
                //_redisCacheService.RemoveData("category");
                var expirationTime = DateTimeOffset.Now.AddMinutes(1);

                var cacheData = _redisCacheService.GetData<IEnumerable<CategoryRecords>>("category");

                if (cacheData != null)
                {
                    return cacheData ?? new List<CategoryRecords>();
                }
                else
                {
                    var sql = "SELECT top 50 * FROM Category";
                    var dbData = await _repository.QueryAsync<Category>(sql);

                    var mappedCategory = _mapper.Map<IEnumerable<CategoryRecords>>(dbData.ToList());

                    _redisCacheService.SetData<IEnumerable<CategoryRecords>>("category", mappedCategory, expirationTime);
                    return mappedCategory;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
