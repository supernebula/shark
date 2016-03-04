using System;
using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Plugin.Pipeline
{
    public class StorageModule : IPageResultModule
    {
        public string ModuleName
        {
            get
            {
                return "仓储模块";
            }
        }

        public string ModuleDescription
        {
            get
            {
                return "数据持久化到数据库，如：SqlServer、MySql、NoSql....";
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
