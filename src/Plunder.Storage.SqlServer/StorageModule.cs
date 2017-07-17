using System;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Plunder.Storage.SqlServer.Models;
using Plunder.Storage.SqlServer.Repositories;

namespace Plunder.Storage.SqlServer
{
    public class StorageModule : IResultPipelineModule
    {
        public string Name => "仓储模块";

        public string Description => "数据持久化到数据库，如：SqlServer、MySql、NoSql....";

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
            var model = ModelBuilder<Product>(pageResult.Data.ToList());
            if (model == null)
                return;
            await Task.Run(() => {
                if ("product".Equals(pageResult.Channel) && pageResult.Data != null && pageResult.Data.Any())
                {
                    var product = model;
                    var repository = new ProductRepository();
                    repository.Insert(product);
                }

                var urls = ConvertToUrl(pageResult.NewRequests);
                var urlRepository = new UrlRepository();
                urlRepository.InsertRange(urls);
            });
        }

        private List<Url> ConvertToUrl(IEnumerable<Request> requests)
        {
            var list = requests.Select(req => new Url()
                {
                    SiteId = req.SiteId,
                    Channel = req.Channel,
                    Hash = req.Hash,
                    Value = req.Url,
                    HttpMethod = req.HttpMethod.Method,
                    UrlType = req.UrlType,
                    Status = UrlStatusType.New,
                    AlreadyRetryCount = 0
                }).ToList();
            return list;
        }

        private static T ModelBuilder<T>(IReadOnlyCollection<ResultField> resultFields) where T : class
        {
            if (resultFields == null || !resultFields.Any())
                return null;
            var obj = (T)Activator.CreateInstance(typeof(T));
            var properties = TypeDescriptor.GetProperties(typeof (T));
            foreach (PropertyDescriptor prop in properties)
            {
                var type = prop.GetType();
                var propName = prop.Name;
                if(!type.IsPublic)
                    continue;
                if (!type.IsEnum && !type.IsPrimitive)
                    continue;
                var tConverter = prop.Converter;
                if (tConverter == null || !tConverter.CanConvertFrom(typeof (string)))
                    continue;
                var field = resultFields.FirstOrDefault(e => e.Name.Equals(propName, StringComparison.CurrentCultureIgnoreCase));
                if (field == null)
                    continue;
                prop.SetValue(obj, tConverter.ConvertFrom(field.Value));
            }
            return obj;
        }
    }
}
