using System;
using StackExchange.Redis;

namespace Plunder.Plugin.Filter
{
    public class RedisClient : IDisposable
    {
        private static RedisClient _instanse;
        public static RedisClient Current
        {
            get
            {
                if (_instanse == null)
                    _instanse = new RedisClient();
                return _instanse;
            }
        }

        private ConnectionMultiplexer _redisConnection;
        public IDatabase Database
        {
            get
            {
                if (_redisConnection == null)
                    throw new NullReferenceException(nameof(_redisConnection));
                if (!_redisConnection.IsConnected)
                    throw new Exception("RedisConnectionException:连接已断开或未连接");
                var db = _redisConnection.GetDatabase();
                return db;
            }
        }

        public void Dispose()
        {
            _redisConnection.Close();
            _redisConnection.Dispose();
        }
    }
}
