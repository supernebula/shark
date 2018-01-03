using Plunder.Compoment;
using Plunder.Configuration;
using Plunder.Pipeline;
using Plunder.Storage.MongoDB.Entities;
using Plunder.Storage.MongoDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Storage.MongoDB
{
    public class PlantCollectPipelineModule : IResultPipelineModule
    {
        public string Name => "植物采集持久化模块";

        public string Description => "植物采集持久化到到数据库";

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            try
            {
                var groups = result.GroupData?.ToList();
                if (groups == null)
                    groups = new List<IEnumerable<ResultField>>();
                if (result.Data != null)
                    groups.Add(result.Data);
                var plantRepos = AppConfig.Current.IocManager.GetService<PlantRepository>();

                groups.ForEach(async e => {
                    await plantRepos.AddAsync(new Plant()
                    {
                        Id = Guid.NewGuid(),
                        LatinName = e.SingleOrDefault(z => z.Name == "LatinName")?.Value ?? string.Empty,
                        ZhName = e.SingleOrDefault(z => z.Name == "ZhName")?.Value ?? string.Empty,
                        Namer = e.SingleOrDefault(z => z.Name == "Namer")?.Value ?? string.Empty,
                        Locality = e.SingleOrDefault(z => z.Name == "Locality")?.Value ?? string.Empty,
                        ListUrl = e.SingleOrDefault(z => z.Name == "ListUrl")?.Value ?? string.Empty,
                        CreateTime = DateTime.Now
                    });
                });

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
