using System;
using System.Security.Cryptography;

namespace Plunder.Utility
{
    public static class RandomUnitily
    {
        static RNGCryptoServiceProvider _RNGCryptoServiceProvider;
        static RandomUnitily()
        {
            _RNGCryptoServiceProvider = new RNGCryptoServiceProvider();
        }
        public static int RealRandom(int min, int max)
        {
            //这样产生min ~ max的强随机数（含max）
            int rnd = int.MinValue;
            decimal _base = (decimal)long.MaxValue;
            byte[] rndSeries = new byte[8];
            _RNGCryptoServiceProvider.GetBytes(rndSeries);
            //不含max需去掉+1 
            rnd = (int)(Math.Abs(BitConverter.ToInt64(rndSeries, min)) / _base * (max + 1));

            return rnd;

            //这个rnd就是你要的随机数，
            //但是要注意别扔到循环里去，实例化RNG对象可是很消耗资源的

        }
    }
}
