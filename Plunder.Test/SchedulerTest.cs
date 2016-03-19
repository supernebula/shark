using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plunder.Analyze;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Pipeline;
using Plunder.Plugin.Downloader;
using Plunder.Scheduler;
using Plunder.Test.Pipeline;

namespace Plunder.Test
{
    [TestClass]
    public class SchedulerTest
    {
        #region test data

        public RequestMessage NewTestRequestMessage(Site site)
        {
            return new RequestMessage()
            {
                Topic = TopicType.STATIC_HTML,
                Request = new Request()
                {
                    Site = site,
                    Uri = "http://www.usashopcn.com/Product/Details/126993"
                }

            };
        }

        public List<RequestMessage> NewTestRequestMessages(Site site)
        {
            var list = new List<RequestMessage>();
            list.Add(new RequestMessage()
            {
                Topic = TopicType.STATIC_HTML,
                Request = new Request()
                { Site = site,  Uri = "http://www.usashopcn.com/Product/Details/126334" }

            });

            list.Add(new RequestMessage()
            {
                Topic = TopicType.STATIC_HTML,
                Request = new Request()
                { Site = site, Uri = "http://www.usashopcn.com/Product/Details/127698" }

            });

            list.Add(new RequestMessage()
            {
                Topic = TopicType.STATIC_HTML,
                Request = new Request()
                { Site = site, Uri = "http://www.usashopcn.com/Product/Details/127593" }

            });

            list.Add(new RequestMessage()
            {
                Topic = TopicType.STATIC_HTML,
                Request = new Request()
                { Site = site, Uri = "http://www.usashopcn.com/Product/Details/126855" }

            });

            return list;
        }

        #endregion

        #region Production Test

        [TestMethod]
        [Description("测试生产，向队列中同步推送单条消息")]
        public void ProductionTest()
        {
            var site = new Site() { Domain = "www.usashopcn.com" };
            var requestMessage = NewTestRequestMessage(site);
            var lineScheduler = new LineScheduler();
            lineScheduler.Push(requestMessage);
            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
            Assert.IsTrue(lineScheduler.CurrentQueueCount() == 1, "添加消息失败");
            lineScheduler.Dispose();
        }

        [TestMethod]
        [Description("测试生产，向队列中异步推送单条消息")]
        public void ProductionAsyncTest()
        {
            var site = new Site() { Domain = "www.usashopcn.com" };
            var requestMessage = NewTestRequestMessage(site);
            var lineScheduler = new LineScheduler();
            lineScheduler.PushAsync(requestMessage);
            Thread.Sleep(500);
            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
            Assert.IsTrue(lineScheduler.CurrentQueueCount() == 1, "添加消息失败");
            lineScheduler.Dispose();
        }

        [TestMethod]
        [Description("测试生产，向队列中同步推送多条消息")]
        public void ProductionMultiTest()
        {
            var site = new Site() { Domain = "www.usashopcn.com" };
            var requestMessages = NewTestRequestMessages(site);
            var lineScheduler = new LineScheduler();
            lineScheduler.Push(requestMessages);
            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
            Assert.IsTrue(lineScheduler.CurrentQueueCount() == requestMessages.Count, "添加消息失败");
            lineScheduler.Dispose();
        }

        [TestMethod]
        [Description("测试生产，向队列中异步推送多条消息")]
        public void ProductionMultiAsyncTest()
        {
            var site = new Site() { Domain = "www.usashopcn.com" };
            var requestMessages = NewTestRequestMessages(site);
            var lineScheduler = new LineScheduler();
            lineScheduler.PushAsync(requestMessages);
            Thread.Sleep(500);
            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
            Assert.IsTrue(lineScheduler.CurrentQueueCount() == requestMessages.Count, "添加消息失败");
            lineScheduler.Dispose();
        }

        #endregion



        #region Consume Test

        public class TestDownloader : IDownloader
        {
            public string Topic => TopicType.STATIC_HTML;

            public async Task<Response> DownloadAsync(Request request)
            {
                Trace.WriteLine("start download:" + request.Uri);
                return await Task.Run(() => new Response() { Content = "这是正文来至于:" + request.Uri, HttpStatusCode = HttpStatusCode.OK, MillisecondTime = 1, ReasonPhrase = "TestReasonPhrase" });

            }

            public bool IsAllowDownload()
            {
                return true;
            }

            public int ThreadCount()
            {
                return 0;
            }
        }

        public class TestPageAnalyzer : IPageAnalyzer
        {
            public Guid Id { get; set; }

            public Site Site { get; set; }

            public PageResult Analyze(Response response)
            {
                Trace.WriteLine("分析正文:" + response.Content);
                return new PageResult();
            }
        }

        public class TestPipelineMoudle : IResultPipelineModule
        {
            public string ModuleDescription => "";

            public string ModuleName => "";

            public void Init(object context)
            {
            }

            public async Task ProcessAsync(PageResult result)
            {
                Trace.WriteLine("处理PageResult");
                await Task.Run(() => result.Result);
            }
        }




        [TestMethod]
        [Description("测试消费者")]
        public void ConsumeTest()
        {
            var site = new Site() {Domain = "www.usashopcn.com"};

            var requestMessages = NewTestRequestMessages(site);
            var lineScheduler = new LineScheduler();
            lineScheduler.Push(requestMessages);
            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());

            var resultPipeline = new ResultPipeline();
            resultPipeline.RegisterModule(new TestPipelineMoudle());
            var pageAnalyzers = new List<KeyValuePair<string, Type>>();
            pageAnalyzers.Add(new KeyValuePair<string, Type>(site.Domain, typeof(TestPageAnalyzer)));
            var consumerBroker = new ConsumerBroker(2, lineScheduler, new List<IDownloader>() {new TestDownloader()}, new ResultPipeline(), pageAnalyzers);
            consumerBroker.StartConsume();

            var timer = new Timer((state) =>
            {
                Trace.WriteLine("Timer结束:");
                Assert.IsTrue(lineScheduler.CurrentQueueCount() == 0, "添加消息失败");
            }, null, 20000, 0);
            
            //lineScheduler.Dispose();
        }

        #endregion
    }
}
