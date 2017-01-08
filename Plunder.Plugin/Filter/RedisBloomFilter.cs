using Plunder.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Plunder.Plugin.Filter
{
    public class RedisBloomFilter<T> : IBloomFilter<T>
    {
        #region Fields

        private IDatabase _database;
        private IDatabase Database
        {
            get
            {
                if(_database == null)
                    _database = RedisClient.Current.GetDatabase(_redisHost, _redisPort);
                return _database;
            }
        }

        /// <summary>
        /// redis 地址
        /// </summary>
        private readonly string _redisHost;

        /// <summary>
        /// redis端口
        /// </summary>
        private readonly int _redisPort ;

        /// <summary>
        /// 位数组
        /// </summary>
        private readonly string _bitSetKey = "plunderBloom";

        /// <summary>
        /// 数据量
        /// </summary>
        private readonly int _dataSize;
        /// <summary>
        /// 空间大小
        /// </summary>
        private readonly int _spaceSize;

        /// <summary>
        /// Hash函数最佳个数
        /// </summary>
        private readonly int _numberOfHashes;

#endregion

        #region Properties

        /// <summary>
        /// 假阳性概率
        /// </summary>
        public double FalsePositiveRate { get; private set; }


        public int DataSize => _dataSize;

        public int SpaceSize => _spaceSize;

        public int NumberOfHashes => _numberOfHashes;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造方法， 自动计算Hash函数最佳个数
        /// </summary>
        /// <param name="dateSize">数据量</param>
        /// <param name="spaceSize">空间量</param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="c;ear">是否清空历史记录</param>
        public RedisBloomFilter(int dateSize, int spaceSize, string host, int port, bool clear = false)
        {
            _dataSize = dateSize;
            _spaceSize = spaceSize;
            _numberOfHashes = OptimalNumberOfHashes();
            FalsePositiveRate = FalsePositiveProbability();
            _redisHost = host;
            _redisPort = port;
            if (clear)
                Clear();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dateSize">数据量</param>
        /// <param name="spaceSize">空间量</param>
        /// <param name="numberOfHashes">Hash函数最佳个数</param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// /// <param name="c;ear">是否清空历史记录</param>
        public RedisBloomFilter(int dateSize, int spaceSize, int numberOfHashes, string host, int port, bool clear = false)
        {
            _dataSize = dateSize;
            _spaceSize = spaceSize;
            _numberOfHashes = numberOfHashes;
            FalsePositiveRate = FalsePositiveProbability();
            _redisHost = host;
            _redisPort = port;
            if (clear)
                Clear();
        }

        ///// <summary>
        ///// 构造方法， 自动计算最佳空间和Hash函数最佳个数
        ///// </summary>
        ///// <param name="dataSize">数据量</param>
        ///// <param name="falsePositiveRate">假阳性概率</param>
        ///// <param name="host"></param>
        ///// <param name="port"></param>
        ///// /// <param name="c;ear">是否清空历史记录</param>
        //[Obsolete("未实现...")]
        //public RedisBloomFilter(int dataSize, float falsePositiveRate, string host, int port, bool clear = false)
        //{
        //    _dataSize = dataSize;
        //    FalsePositiveRate = falsePositiveRate;
        //    _redisHost = host;
        //    _redisPort = port;
        //    if (clear)
        //        Clear();
        //    throw new NotImplementedException();
        //}

        public void Clear()
        {
            var beforeCount = Database.StringBitCount("_bitSetKey");
            Database.KeyDelete(_bitSetKey);
            var afterCount = Database.StringBitCount("_bitSetKey");
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"RedisBloomFilter, 清理前{beforeCount}, 清理后{afterCount}");
            Console.ForegroundColor = originalColor;
        }

        #endregion

        public void Add(T item)
        {
            var random = new Random(Hash(item));
            for (int i = 0; i < _numberOfHashes; i++)
            {
                var offset = random.Next(_spaceSize);
                Database.StringSetBit(_bitSetKey, offset, true);
            }
        }

        public bool Contains(T item)
        {
            var random = new Random(Hash(item));
            for (int i = 0; i < _numberOfHashes; i++)
            {
                var offset = random.Next(_spaceSize);
                if (Database.StringGetBit(_bitSetKey, offset))
                    return true;
            }
            return false;
        }

        public bool ContainsAll(IEnumerable<T> items)
        {
            return items.All(Contains);
        }

        public bool ContainsAny(IEnumerable<T> items)
        {
            return items.Any(Contains);
        }

        public double FalsePositiveProbability()
        {
            return Math.Pow((1 - Math.Exp(-_numberOfHashes * _dataSize / (double)_spaceSize)), _numberOfHashes);

        }

        public int OptimalNumberOfHashes()
        {
            return (int)Math.Ceiling((_spaceSize * 1.00 / _dataSize) * Math.Log(2.0));
        }

        private int Hash(T item)
        {
            return item.GetHashCode();
        }
    }
}
