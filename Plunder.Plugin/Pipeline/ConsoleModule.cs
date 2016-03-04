using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Plugin.Pipeline
{
    public class ConsoleModule : IPageResultModule
    {
        public string ModuleName
        {
            get
            {
                return "控制台模块";
            }
        }

        public string ModuleDescription
        {
            get
            {
                return "输出信息到控制台";
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

        public void Process(IPageResult result)
        {
            throw new NotImplementedException();
        }
    }
}
