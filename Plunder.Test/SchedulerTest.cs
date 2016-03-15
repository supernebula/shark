using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plunder.Compoment;
using Plunder.Plugin.Downloader;
using Plunder.Scheduler;

namespace Plunder.Test
{
    [TestClass]
    public class SchedulerTest
    {
        [TestMethod]
        [Description("")]
        public void ProductionTest()
        {

            var requestMessage = new RequestMessage()
            {
                Topic = TopicType.STATIC_HTML,
                Request = new Request()
                {
                    Site = new Site() { Domain = "www.usashopcn.com"},
                    Uri = "http://www.usashopcn.com/Product/Details/126334"
                }

            };
            var lineScheduler = new LineScheduler();
            
            lineScheduler.Push(requestMessage);

            Thread.Sleep(1000);
            Assert.IsTrue(lineScheduler.CurrentQueueCount() == 1, "添加消息失败");
            lineScheduler.Dispose();

        }
    }
}
