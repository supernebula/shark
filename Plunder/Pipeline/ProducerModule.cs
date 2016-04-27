using System;
using System.Linq;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Scheduler;

namespace Plunder.Pipeline
{
    public class ProducerModule : IResultPipelineModule
    {
        private readonly IScheduler _scheduler;

        public ProducerModule(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public string ModuleName => "生产者模块";

        public string ModuleDescription => "从结果中发现新的url并封装为请求消息，交付给队列";

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            await _scheduler.PushAsync(result.NewRequests.Select(e => new RequestMessage()
            {
                Topic = result.Topic,
                Request = e
            })).ContinueWith(t => Console.WriteLine("ProducerPushAsync:" + result.Request.Url));
        }
    }
}
