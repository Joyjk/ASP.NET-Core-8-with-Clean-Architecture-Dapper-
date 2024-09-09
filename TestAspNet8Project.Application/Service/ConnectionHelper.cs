using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAspNet8Project.Application.Service
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            //ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            //{
            //    return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisCacheUrl"] ??"");
            //    //return ConnectionMultiplexer.Connect("redis-19587.c258.us-east-1-4.ec2.redns.redis-cloud.com:19587,password=yjRGuK3Xmrp1jFoToMdN6Zh5zjqwXTQt");
            //    //return ConnectionMultiplexer.Connect("127.0.0.1:6379");
            //});

            lazyConnection = new Lazy<ConnectionMultiplexer>(() => TryConnectToRedis());
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {

            get
            {
                return lazyConnection.Value;
            }
        }
        private static ConnectionMultiplexer TryConnectToRedis()
        {
            //string redisCacheUrl = ConfigurationManager.AppSettings["RedisCacheUrl"];
            string redisCacheUrl = "127.0.0.1:6379";
            if (string.IsNullOrEmpty(redisCacheUrl))
            {
                Console.WriteLine("RedisCacheUrl is not configured in the app settings.");
                return null; // Or throw an exception if required
            }

            try
            {
                return ConnectionMultiplexer.Connect(redisCacheUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging framework)
                Console.WriteLine($"Error connecting to Redis: {ex.Message}");
                return null; // Return null or handle it as per your application's needs
            }
        }
        public static bool IsConnected
        {
            get
            {
                return Connection != null && Connection.IsConnected;
            }
        }
    }
}
