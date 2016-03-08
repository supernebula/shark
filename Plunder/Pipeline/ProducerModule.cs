using System;
using System.Linq;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Scheduler;

namespace Plunder.Pipeline
{
    public class ProducerModule : IResultPipelineModule
    {

        public ProducerModule(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        private IScheduler _scheduler;

        public string ModuleName
        {
            get { return "生产者模块"; }
        }

        public string ModuleDescription
        {
            get { return "从结果中发现新的url并封装为请求消息，交付给调度器消息队列"; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync<T>(PageResult<T> result)
        {
            await _scheduler.PushAsync(result.NewRequest.Select(e => new RequestMessage()
            {
                Id = Guid.NewGuid(),
                Topic = "simple",
                Request = e
            }));
        }
    }
}
