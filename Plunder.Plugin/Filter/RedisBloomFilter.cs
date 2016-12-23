using System;
using System.Collections.Generic;
using Plunder.Filter;

namespace Plunder.Plugin.Filter
{
    public class RedisBloomFilter<T> : IBloomFilter<T>
    {
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsAll(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public bool ContainsAny(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public double FalsePositiveProbability()
        {
            throw new NotImplementedException();
        }

        public int OptimalNumberOfHashes()
        {
            throw new NotImplementedException();
        }
    }
}
