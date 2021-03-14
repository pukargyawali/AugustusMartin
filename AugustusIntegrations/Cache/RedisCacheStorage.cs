using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AugustusIntegrations.Cache
{
    public class RedisCacheStorage : ICacheStorage
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private IDatabase _redis;
        private ILogger<RedisCacheStorage> _logger;

        public RedisCacheStorage(ConnectionMultiplexer redisConnection, ILogger<RedisCacheStorage> logger)
        {
            _redisConnection = redisConnection;
            _redis = redisConnection.GetDatabase(0);
            _logger = logger;
        }

        public async Task<T> AddValueAsync<T>(string key, T value)
        {
            try
            {
                var updatedCode = await _redis.
                                    StringSetAsync(key, JsonConvert.SerializeObject(value));
                if (!updatedCode)
                {
                    return default(T);
                }
                return await GetValueAsync<T>(key);
            }
            catch (RedisException rEx)
            {
                _logger.Log(LogLevel.Error, "Error occured while adding data to redis(" + rEx.Message + ")", rEx);
                throw new Exception("Error occured while adding data to redis(" + rEx.Message + ")", rEx);
            }

        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            try
            {
                var data = await _redis.StringGetAsync(key);
                if (data.IsNullOrEmpty)
                {
                    return default(T);
                }
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (RedisException rEx)
            {
                _logger.Log(LogLevel.Error, "Error occured while accessing data to redis(" + rEx.Message + ")", rEx);
                throw new Exception(" Error occured while accesing data from redis(" + rEx.Message + ")", rEx);
            }
        }

        public async Task<bool> RemoveValueAsync(string key)
        {
            try
            {
                return await _redis.KeyDeleteAsync(key);
            }
            catch (RedisException rEx)
            {
                _logger.Log(LogLevel.Error, "Error occured while deleting data to redis(" + rEx.Message + ")", rEx);
                throw new Exception(" Error occured while deleting data from redis(" + rEx.Message + ")", rEx);
            }

        }
    }
}
