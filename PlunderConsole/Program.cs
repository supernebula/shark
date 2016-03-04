using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Scheduler;
using Plunder;
using Plunder.Plugin.Pipeline;
using Plunder.Plugin.Downloader;

namespace PlunderConsole
{
    public class Program
    {
        static Spider _spider;

        static void Main(string[] args)
        {
            _spider = new Spider(new LineScheduler());
            _spider.RegisterDownloader<HttpSimpleDownloader>("simpleDownload");
            _spider.RegisterDownloader<HttpDynamicDownloader>("dynamicDownload");
            //_spider.AddPipeLineModule(new ProducerModule(), new ConsoleModule());
            _spider.Start();
        }
    }
}   