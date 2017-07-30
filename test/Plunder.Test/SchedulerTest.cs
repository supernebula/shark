//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Net;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Plunder.Process.Analyze;
//using Plunder.Compoment;
//using Plunder.Download;
//using Plunder.Pipeline;
//using Plunder.Plugin.Download;
//using Plunder.Schedule;
//using Plunder.Schedule.Filter;
//using Plunder.Test.Pipeline;
//using Plunder.Download.Proxy;

//namespace Plunder.Test
//{
//    [TestClass]
//    public class SchedulerTest
//    {
//        #region test data

//        public RequestMessage NewTestRequestMessage(Site site)
//        {
//            return new RequestMessage()
//            {
                
//                Request = new Request("http://www.usashopcn.com/Product/Details/126993")
//                {
//                    SiteId = site.Id,
//                }

//            };
//        }

//        public List<RequestMessage> NewTestRequestMessages(Site site)
//        {
//            var list = new List<RequestMessage>();
//            list.Add(new RequestMessage()
//            {
//                Request = new Request("http://www.usashopcn.com/Product/Details/126334")
//                { SiteId = site.Id}

//            });

//            list.Add(new RequestMessage()
//            {
//                Request = new Request("http://www.usashopcn.com/Product/Details/127698")
//                { SiteId = site.Id}

//            });

//            list.Add(new RequestMessage()
//            {
//                Request = new Request("http://www.usashopcn.com/Product/Details/127593")
//                { SiteId = site.Id}

//            });

//            list.Add(new RequestMessage()
//            {
//                Request = new Request("http://www.usashopcn.com/Product/Details/126855")
//                { SiteId = site.Id }

//            });

//            return list;
//        }

//        #endregion

//        #region Production Test

//        [TestMethod]
//        [Description("测试生产，向队列中同步推送单条消息")]
//        public void ProductionTest()
//        {
//            var site = new Site() { Domain = "www.usashopcn.com" };
//            var requestMessage = NewTestRequestMessage(site);
//            var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
//            var lineScheduler = new SequenceScheduler(bloomFilter, default(EngineContext));
//            lineScheduler.Push(requestMessage);
//            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
//            Assert.IsTrue(lineScheduler.CurrentQueueCount() == 1, "添加消息失败");
//            lineScheduler.Dispose();
//        }

//        [TestMethod]
//        [Description("测试生产，向队列中异步推送单条消息")]
//        public void ProductionAsyncTest()
//        {
//            var site = new Site() { Domain = "www.usashopcn.com" };
//            var requestMessage = NewTestRequestMessage(site);
//            var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
//            var lineScheduler = new SequenceScheduler(bloomFilter, default(EngineContext));
//            lineScheduler.PushAsync(requestMessage);
//            Thread.Sleep(500);
//            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
//            Assert.IsTrue(lineScheduler.CurrentQueueCount() == 1, "添加消息失败");
//            lineScheduler.Dispose();
//        }

//        [TestMethod]
//        [Description("测试生产，向队列中同步推送多条消息")]
//        public void ProductionMultiTest()
//        {
//            var site = new Site() { Domain = "www.usashopcn.com" };
//            var requestMessages = NewTestRequestMessages(site);
//            var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
//            var lineScheduler = new SequenceScheduler(bloomFilter, default(EngineContext));
//            lineScheduler.Push(requestMessages);
//            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
//            Assert.IsTrue(lineScheduler.CurrentQueueCount() == requestMessages.Count, "添加消息失败");
//            lineScheduler.Dispose();
//        }

//        [TestMethod]
//        [Description("测试生产，向队列中异步推送多条消息")]
//        public void ProductionMultiAsyncTest()
//        {
//            var site = new Site() { Domain = "www.usashopcn.com" };
//            var requestMessages = NewTestRequestMessages(site);
//            var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
//            var lineScheduler = new SequenceScheduler(bloomFilter, default(EngineContext));
//            lineScheduler.PushAsync(requestMessages);
//            Thread.Sleep(500);
//            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());
//            Assert.IsTrue(lineScheduler.CurrentQueueCount() == requestMessages.Count, "添加消息失败");
//            lineScheduler.Dispose();
//        }

//        #endregion



//        #region Consume Test

//        public class TestDownloader : IDownloader
//        {
//            public int DownloadingTaskCount
//            {
//                get
//                {
//                    throw new NotImplementedException();
//                }
//            }

//            public bool IsDefault { get; set; }

//            //public string ContentType => WPageType.Static;

//            public PageType PageType { get; set; }

//            public Request Request => throw new NotImplementedException();

//            public UserAgent UserAgent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//            public HttpProxy Proxy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//            public DownloadStatus Status => throw new NotImplementedException();

//            public DateTime? DownloadStartTime => throw new NotImplementedException();

//            public int HasElapsed => throw new NotImplementedException();

//            public async Task<Response> DownloadAsync(Request request)
//            {
//                Trace.WriteLine("start download:" + request.Url);
//                return await Task.Run(() => new Response() { Content = "这是正文来至于:" + request.Url, HttpStatusCode = HttpStatusCode.OK, Elapsed = 1, ReasonPhrase = "TestReasonPhrase" });

//            }

//            public Task<Response> DownloadAsync(CancellationToken token)
//            {
//                throw new NotImplementedException();
//            }

//            //public Task DownloadAsync(Request requests, Action<Request, Response> onDownloaded)
//            //{
//            //    throw new NotImplementedException();
//            //}


//            //public void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> onDownloaded, Action onConsumed)
//            //{
//            //    throw new NotImplementedException();
//            //}

//            //public Task<Response> DownloadAsync(CancellationToken token)
//            //{
//            //    throw new NotImplementedException();
//            //}

//            public bool IsAllowDownload()
//            {
//                return true;
//            }

//            public void ReInit()
//            {
//                throw new NotImplementedException();
//            }

//            public int TaskCount()
//            {
//                return 0;
//            }
//        }

//        public class TestPageAnalyzer : IPageAnalyzer
//        {
//            public Guid Id { get; set; }

//            public Site Site { get; set; }

//            public string SiteId => throw new NotImplementedException();

//            public string PageTag => throw new NotImplementedException();

//            public string TargetPageFlag => throw new NotImplementedException();

//            public PageResult Analyze(Request request, Response response)
//            {
//                Trace.WriteLine("分析正文:" + response.Content);
//                return new PageResult();
//            }

//            public PageResult Analyze(Response response)
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public class TestPipelineMoudle : Plunder.Pipeline.IResultPipelineModule
//        {
//            public string Description => "";

//            public string Name => "";

//            public void Init(object context)
//            {
//            }

//            public async Task ProcessAsync(PageResult result)
//            {
//                Trace.WriteLine("处理PageResult");
//                await Task.Run(() => result.Data);
//            }
//        }




//        [TestMethod]
//        [Description("测试消费者")]
//        public void ConsumeTest()
//        {
//            var site = new Site() {Domain = "www.usashopcn.com"};

//            var requestMessages = NewTestRequestMessages(site);
//            var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
//            var lineScheduler = new SequenceScheduler(bloomFilter, default(EngineContext));
//            lineScheduler.Push(requestMessages);
//            Trace.WriteLine("CurrentQueueCount:" + lineScheduler.CurrentQueueCount());

//            var resultPipeline = new ResultItemPipeline();
//            resultPipeline.RegisterModule(new TestPipelineMoudle());
//            var pageAnalyzers = new List<KeyValuePair<string, Type>>();
//            pageAnalyzers.Add(new KeyValuePair<string, Type>(site.Domain, typeof(TestPageAnalyzer)));
//            var consumerBroker = new ConsumerBroker(2, lineScheduler, new List<IDownloaderOld>() {new TestDownloader()}, new ResultItemPipeline(), pageAnalyzers);
//            consumerBroker.Start();

//            var timer = new Timer((state) =>
//            {
//                Trace.WriteLine("Timer结束:");
//                Assert.IsTrue(lineScheduler.CurrentQueueCount() == 0, "添加消息失败");
//            }, null, 20000, 0);
            
//            //lineScheduler.Dispose();
//        }

//        #endregion
//    }
//}
