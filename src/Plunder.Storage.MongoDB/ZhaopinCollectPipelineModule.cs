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


                    var repos = AppConfig.Current.IocManager.GetService<ZhaopinJobRepository>();
                    var positionId = result.Data.SingleOrDefault(z => z.Name == "PositionId")?.Value ?? string.Empty;
                    var industry = result.Data.SingleOrDefault(z => z.Name == "Industry")?.Value ?? string.Empty;
                    var financing = result.Data.SingleOrDefault(z => z.Name == "Financing")?.Value ?? string.Empty;

                    var staffNumber = result.Data.SingleOrDefault(z => z.Name == "StaffNumber")?.Value ?? string.Empty;
                    var siteUrl = result.Data.SingleOrDefault(z => z.Name == "SiteUrl")?.Value ?? string.Empty;
                    var site = result.Data.SingleOrDefault(z => z.Name == "Site")?.Value ?? string.Empty;
                    var city = result.Data.SingleOrDefault(z => z.Name == "City")?.Value ?? string.Empty;
                    var title = result.Data.SingleOrDefault(z => z.Name == "Title")?.Value ?? string.Empty;
                    var minSalaryStr = result.Data.SingleOrDefault(z => z.Name == "MinSalary")?.Value;
                    var minSalary = 0;
                    Int32.TryParse(minSalaryStr, out minSalary);
                    var maxSalaryStr = result.Data.SingleOrDefault(z => z.Name == "MaxSalary")?.Value;
                    var maxSalary = 0;
                    Int32.TryParse(maxSalaryStr, out maxSalary);
                    var workYear = result.Data.SingleOrDefault(z => z.Name == "WorkYear")?.Value;
                    var education = result.Data.SingleOrDefault(z => z.Name == "Education")?.Value;
                    var positionInfo = result.Data.SingleOrDefault(z => z.Name == "PositionInfo")?.Value;
                    var address = result.Data.SingleOrDefault(z => z.Name == "Address")?.Value;

                    await repos.DeleteByAsync(e => e.PositionId == positionId);

                    await repos.AddAsync(new LagouJob()
                    {
                        Id = Guid.NewGuid(),
                        PositionId = positionId,
                        Industry = industry,
                        Financing = financing,
                        StaffNumber = staffNumber,
                        SiteUrl = site,
                        Site = site,
                        City = city,
                        Title = title,
                        MinSalary = minSalary,
                        MaxSalary = maxSalary,
                        WorkYear = workYear,
                        Education = education,
                        PositionInfo = positionId,
                        Address = address,
                        CreateTime = DateTime.Now
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
