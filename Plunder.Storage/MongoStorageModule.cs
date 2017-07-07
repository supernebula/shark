using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Storage
{
    public class MongoStorageModule : IResultPipelineModule
    {
        public string Description => "MongoDB存储结果模块";

        public string Name => "MongoDB存储结果模块";

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync(PageResult result)
        {
            //using (var contenxt = new PlunderMongoDBContext())
            //{
                
            //}
            throw new NotImplementedException();
        }
    }
}
