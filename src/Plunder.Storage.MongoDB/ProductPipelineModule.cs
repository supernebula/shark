using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;
using Plunder.Storage.MongoDB.Repositories;
using Evol.Utilities.Hash;
using Plunder.Storage.MongoDB.Entities;

namespace Plunder.Storage.MongoDB
{
    public class ProductPipelineModule : IResultPipelineModule
    {
        public string Description => "Product存储结果模块";

        public string Name => "Product存储结果模块";

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            try
            {
                
                List<string> picUrls = null;
                var picUrl = result.Data.FirstOrDefault(e => e.Name == "PicUrl")?.Value;
                if (!string.IsNullOrWhiteSpace(picUrl))
                    picUrls = new List<string> { picUrl };

                double price = 0;
                double.TryParse(result.Data.FirstOrDefault(e => e.Name == "Price")?.Value, out price);

                var productRepos = AppConfig.Current.IocManager.GetService<ProductRepository>();
                await productRepos.AddAsync(new Product()
                {
                    Id = Guid.NewGuid(),
                    Title = result.Data.FirstOrDefault(e => e.Name == "Title")?.Value,
                    Domain = result.Response.Request.Domain,
                    Url = result.Request.Url,
                    UrlSign = HashUtility.Md5(result.Request.Url),
                    PicUrl = picUrls,
                    Price = price,
                    Description = result.Data.FirstOrDefault(e => e.Name == "Description")?.Value.Replace(" ",""),
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

