using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Scheduler;
using Plunder;

namespace PlunderConsole
{
    public class Program
    {
        static Spider _spider;

        static void Main(string[] args)
        {
            _spider = new Spider(new QueueScheduler());
            _spider.Run();
        }
    }
}