using System;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;
using System.Collections.Generic;
using System.Linq;
using Plunder.Plugin.Storage.Models;
using Plunder.Plugin.Storage.Repositories;

namespace Plunder.Plugin.Storage
{
    public class StorageModule : IResultPipelineModule
    {
        public string ModuleName => "仓储模块";

        public string ModuleDescription => "数据持久化到数据库，如：SqlServer、MySql、NoSql....";

        public StorageModule()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult pageResult)
        {
            await Task.Run(() => {
                if ("product".Equals(pageResult.Channel) && pageResult.Data != null && pageResult.Data.Any())
                {
                    var product = ModelBuilder<Product>(pageResult.Data);
                    var repository = new ProductRepository();
                    repository.Insert(product);
                }
            });
        }

        private T ModelBuilder<T>(IEnumerable<ResultField> resultField)
        {
            throw new NotImplementedException();
        }
    }
}
