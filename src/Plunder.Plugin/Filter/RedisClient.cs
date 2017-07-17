using System;
using System.Collections.Concurrent;
using StackExchange.Redis;

namespace Plunder.Plugin.Filter
{
    public class RedisClient : IDisposable
    {
        private static RedisClient _instanse;

        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _redisConnectionCollection = new ConcurrentDictionary<string, ConnectionMultiplexer>();

        public static RedisClient Current
        {
            get
            {
                if (_instanse == null)
                    _instanse = new RedisClient();
                return _instanse;
            }
        }

        public IDatabase GetDatabase(string host, int port)
        {
            var configuration = $"{host}:{port}";
            ConnectionMultiplexer connection;
            if (!_redisConnectionCollection.ContainsKey(configuration))
            {
                connection = ConnectionMultiplexer.Connect(configuration);
                if (!_redisConnectionCollection.TryAdd(configuration, connection))
                {
                    connection.Close();
                    throw new InvalidOperationException("连接添加到redis连接池失败");
                }

                return connection.GetDatabase();
            }
           

            if(_redisConnectionCollection.TryGetValue(configuration, out connection))
                throw new InvalidOperationException($"从连接池{nameof(_redisConnectionCollection)}获取连接失败");
            if(connection == null)
                throw new NullReferenceException($"返回的连接为空{(ConnectionMultiplexer) null}");
            return connection.GetDatabase();
        }


        public void Dispose()
        {
            foreach (var connect in _redisConnectionCollection.Values)
            {
                connect.Close();
                connect.Dispose();
            }
        }
    }
}
