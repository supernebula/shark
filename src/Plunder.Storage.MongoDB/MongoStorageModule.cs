using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;
using Plunder.Storage.MongoDB.Repositories;
using Plunder.Compoment.Models;

namespace Plunder.Storage.MongoDB
{
    public class MongoStorageModule : IResultPipelineModule
    {
        public string Description => "MongoDB存储结果模块";

        public string Name => "MongoDB存储结果模块";

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            var accesslogRepos = new AccessLogRepository();
            await accesslogRepos.AddAsync(new AccessLog() {
                Id = Guid.NewGuid().ToString().ToLower(),
                Domian = result.Request.Domain,
                Uri = result.Request.Url,
                StatusCode = result.Response.HttpStatusCode,
                IsSuccessCode = result.Response.IsSuccessCode,
                Elapsed = result.Response.Elapsed,
                CreateTime = DateTime.Now
            });

            var pageRepos = new PageRepository();
            await pageRepos.AddAsync(new Page() {
                Id = Guid.NewGuid().ToString().ToLower(),
                Domain = result.Request.Domain,
                Uri = result.Request.Url,
                UriSign = result.Request.Hash,
                IsFetched = result.Response.IsSuccessCode,
                Elapsed = result.Response.Elapsed,
                Content = result.Response.Content,
                CreateTime = DateTime.Now
            });
            //using (var contenxt = new PlunderMongoDBContext())
            //{
                
            //}
            throw new NotImplementedException();
        }
    }
}
