using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    internal static class DownloaderFactory
    {

        private static Dictionary<string, Func<string, IDownloader>> _creatorDic;

        static DownloaderFactory()
        {
            _creatorDic = new Dictionary<string, Func<string, IDownloader>>();
        }
        public static IDownloader Create(string topic)
        {
            var func = _creatorDic[topic];
            if (func == null)
                throw new ArgumentException("没有注册次topic对应的创建委托：" + topic);
            return func.Invoke(topic);
        }

        public static int Count()
        {
            return _creatorDic.Count();
        }

        public static void RegisterCreator(string topic, Func<string, IDownloader> createFunc)
        {
            _creatorDic.Add(topic, createFunc);
        }

    }
}
