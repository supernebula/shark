using NLog;
using Plunder.Compoment;
using Plunder.Configuration;
using Plunder.Pipeline;
using Plunder.Storage.MongoDB.Entities;
using Plunder.Storage.MongoDB.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Storage.MongoDB
{
    public class ZhaopinCollectPipelineModule : IResultPipelineModule
    {
        public string Name => "招聘岗位采集持久化模块";

        public string Description => "招聘岗位采集持久化模块";

        private ILogger Logger = LogManager.GetLogger("ZhaopinCollectPipelineModule");

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            try
            {
                if (result.Topic != "lagou.list" && result.Topic != "lagou.detail")
                    return;



                if (result.Topic == "lagou.detail")
                {
                    var groups = result.GroupData?.ToList();
                    if (groups == null)
                        groups = new List<IEnumerable<ResultField>>();
                    if (result.Data != null)
                        groups.Add(result.Data);


                    var plantPhotoRepos = AppConfig.Current.IocManager.GetService<PlantPhotoRepository>();

                    groups.ForEach(async e => {

                        var latinName = e.SingleOrDefault(z => z.Name == "LatinName")?.Value ?? string.Empty;
                        var sourceSite = e.SingleOrDefault(z => z.Name == "SourceSite")?.Value ?? string.Empty;

                        var thumbUrl = e.SingleOrDefault(z => z.Name == "ThumbImgUrl")?.Value ?? string.Empty;
                        var thumbPath = e.SingleOrDefault(z => z.Name == "ThumbPath")?.Value;
                        var normalUrl = e.SingleOrDefault(z => z.Name == "NormalImgUrl")?.Value;
                        var normalPath = e.SingleOrDefault(z => z.Name == "NormalPath")?.Value;

                        var exitItem = await plantPhotoRepos.FindOneAsync(i => i.ThumbUrl == thumbUrl);
                        if (exitItem != null)
                            return;

                        await plantPhotoRepos.AddAsync(new PlantPhoto()
                        {
                            Id = Guid.NewGuid(),
                            LatinName = latinName,
                            SourceSite = sourceSite,
                            ThumbUrl = thumbUrl ?? string.Empty,
                            NormalUrl = normalUrl,
                            CreateTime = DateTime.Now
                        });
                    });
                }



            }
            catch (Exception ex)
            {
                Logger.Debug("Exception:拉勾网模块:" + ex.Message);
                throw;
            }
        }
    }
}
