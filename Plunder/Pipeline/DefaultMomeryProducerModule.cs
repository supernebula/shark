using System;
using System.Linq;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Schedule;

namespace Plunder.Pipeline
{
    public class DefaultMomeryProducerModule : IProducerModule, IPipelineModule
    {
        public IScheduler Scheduler { get; private set; }

        public DefaultMomeryProducerModule(IScheduler scheduler)
        {
            Scheduler = scheduler;
        }

        public string Name => "默认内存生产者模块";

        public string Description => "从结果中发现新的url并封装为请求消息，交付给队列";

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
            var reqMegs = result.NewRequests.Select(e => new RequestMessage()
            {
                Topic = result.Topic,
                Request = e
            }).ToArray();

            await ScheduleAsync(reqMegs);
        }

        public async Task ScheduleAsync(params RequestMessage[] reqMsgs)
        {
            await Scheduler.PushAsync(reqMsgs).ContinueWith(t =>
            {
                foreach (var item in reqMsgs)
                {
                    Console.WriteLine("ProducerPushAsync:" + item.Request.Url);
                }
            });
        }
    }
}
