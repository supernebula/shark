using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;
using Plunder.Storage.MongoDB.Repositories;
using Plunder.Compoment.Models;
using Evol.Utilities.Hash;

namespace Plunder.Storage.MongoDB
{
    public class MongoDBPipelineModule : IResultPipelineModule
    {
        public string Description => "MongoDB存储结果模块";

        public string Name => "MongoDB存储结果模块";

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            try
            {
                var accesslogRepos = AppConfig.Current.IocManager.GetService<AccessRecordRepository>();
                await accesslogRepos.AddAsync(new AccessRecord()
                {
                    Id = Guid.NewGuid(),
                    Domian = result.Request.Domain,
                    Url = result.Request.Url,
                    UrlSign = HashUtility.Md5(result.Request.Url),
                    UrlType = result.Request.UrlType,
                    PageType = result.Request.PageType.ToString(),
                    NewTargetUrlNum = result.NewRequests.Count(e => e.UrlType == UrlType.Target),
                    Downloader = result.Request.Downloader,
                    IsFetched = !string.IsNullOrWhiteSpace(result.Response.Content),
                    StatusCode = result.Response.HttpStatusCode,
                    IsSuccessCode = result.Response.IsSuccessCode,
                    Elapsed = result.Response.Elapsed,
                    Content = result.Response.Content,
                    CreateTime = DateTime.Now
                });
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
