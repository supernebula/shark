using Plunder.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Plugin.Pipeline
{
    public class ConsoleResultModule : IResultPipelineModule
    {
        public string Description => "简单控制台结果中间件";

        public string Name => "简单控制台结果中间件";

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync(PageResult result)
        {
            Console.WriteLine($"{result.Request.Url}");
            return Task.FromResult(1);
        }
    }
}
