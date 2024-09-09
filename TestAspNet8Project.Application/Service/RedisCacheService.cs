using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspNet8Project.Domain.Interface;

namespace TestAspNet8Project.Application.Service
{
    public class RedisCacheService : IRedisCacheRepository
    {
        private IDatabase _db;
        public RedisCacheService()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            var connection = ConnectionHelper.Connection;
            if (connection != null && connection.IsConnected)
            {
                _db = connection.GetDatabase();
            }
            else
            {
                // Handle the case where Redis is not connected
                // You can log the issue or throw an exception
                Console.WriteLine("Redis connection is not available. Redis operations will not be performed.");
                _db = null;
            }
        }
        public T GetData<T>(string key)
        {
            if (_db == null)
            {
                // Handle the situation where Redis is not connected
                Console.WriteLine("Cannot get data. Redis is not connected.");
                return default;
            }

            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {

            if (_db == null)
            {
                // Handle the situation where Redis is not connected
                Console.WriteLine("Cannot set data. Redis is not connected.");
                return false;
            }

            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            if (_db == null)
            {
                // Handle the situation where Redis is not connected
                Console.WriteLine("Cannot remove data. Redis is not connected.");
                return false;
            }

            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}
