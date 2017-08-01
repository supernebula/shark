using Plunder.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Storage.MongoDB
{
    class MongoPipelineModule2 : IResultPipelineModule
    {
        public string Name => "MongoDB存储中间件";

        public string Description => "MongoDB存储中间件";

        public void Init(object context)
        {

        }

        public Task ProcessAsync(PageResult result)
        {
            throw new NotImplementedException();
        }
    }
}
