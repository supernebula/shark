﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Scheduler;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Proxy;

namespace Plunder
{
    public class Spider
    {
        private IScheduler _scheduler;
        public Spider(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }
        public void Run()
        {
           
        }
    }
}