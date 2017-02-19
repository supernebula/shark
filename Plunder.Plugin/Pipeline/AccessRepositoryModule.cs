using Plunder.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Plugin.Repositories;
using Plunder.Models;
using Evol.Utilities;
using Evol.Utilities.Hash;

namespace Plunder.Plugin.Pipeline
{
    public class AccessRepositoryModule : IPipelineModule
    {
        public IAccessLogRepository AccessLogRepository { get; set; }
        string IPipelineModule.Description => "存储访问日志记录";

        string IPipelineModule.Name => "访问日志存储";

        void IPipelineModule.Init(object context)
        {
            
        }

        async Task IPipelineModule.ProcessAsync(PageResult result)
        {
            var accessLog = new AccessLog() {
                Id = Guid.NewGuid().ToString(),
                Domian = result.Request.Domain,
                Uri = result.Request.Url,
                StatusCode = result.Response.HttpStatusCode,
                IsSuccessCode = result.Response.IsSuccessCode,
                ElapsedTime = result.Response.MillisecondTime,
                CreateTime = DateTime.Now
            };
            await AccessLogRepository.AddAsync(accessLog);

            var page = new Page()
            {
                Topic = result.Topic,
                Domain = result.Request.Domain,
                Uri = result.Request.Url,
                UriSign = HashUtility.Md5(result.Request.Url),
                IsFetched = result.Response.IsSuccessCode,
                Elapsed = result.Response.MillisecondTime,
                CreateTime = DateTime.Now
            };

            await AccessLogRepository.AddAsync(accessLog);
        }
    }
}
