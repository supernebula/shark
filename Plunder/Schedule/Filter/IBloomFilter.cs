using System.Collections.Generic;

namespace Plunder.Schedule.Filter
{
    /// <summary>
    /// 布隆过滤器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBloomFilter<in T> : IDuplicateFilter<T>
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// 判断是否包含项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(T item);

        /// <summary>
        /// 判断是否包含任何一个项目
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool ContainsAny(IEnumerable<T> items);

        /// <summary>
        /// 判断是否包含多个项目
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool ContainsAll(IEnumerable<T> items);

        /// <summary>
        /// 假阳性概率
        /// </summary>
        /// <returns></returns>
        double FalsePositiveProbability();


        /// <summary>
        /// Hash函数数量
        /// </summary>
        /// <returns></returns>
        int OptimalNumberOfHashes();

    }
}
