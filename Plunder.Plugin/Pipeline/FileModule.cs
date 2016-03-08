using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Plugin.Pipeline
{
    public class FileModule : IResultPipelineModule
    {
        public string ModuleName
        {
            get
            {
                return "文件存储模块";
            }
        }

        public string ModuleDescription
        {
            get
            {
                return "数据持久化到文件";
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync<T>(PageResult<T> data)
        {
            throw new NotImplementedException();
        }
    }
}
