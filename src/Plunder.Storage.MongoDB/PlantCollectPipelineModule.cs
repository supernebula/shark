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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Storage.MongoDB
{
    public class PlantCollectPipelineModule : IResultPipelineModule
    {
        public string Name => "植物采集持久化模块";

        public string Description => "植物采集持久化到到数据库";

        private ILogger Logger = LogManager.GetLogger("PlantCollectPipelineModule");

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult result)
        {
            try
            {
                if (result.Topic != "plantnames.list" && result.Topic != "plantname.detail")
                    return;

                if (result.Topic == "plantnames.list")
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

                if (result.Topic == "plantname.detail")
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
                        var thumbLocalPath = await SaveImageAsync(thumbPath, thumbUrl);
                        var normalLocalPath = await SaveImageAsync(normalPath, normalUrl);

                        await plantPhotoRepos.AddAsync(new PlantPhoto()
                        {
                            Id = Guid.NewGuid(),
                            LatinName = latinName,
                            SourceSite = sourceSite,
                            ThumbUrl = thumbUrl ?? string.Empty,
                            NormalUrl = normalUrl,
                            ThumbPath = thumbLocalPath,
                            NormalPath = normalLocalPath,
                            CreateTime = DateTime.Now
                        });
                    });
                }



            }
            catch (Exception ex)
            {
                Logger.Debug("Exception:植物采集持久化模块:" + ex.Message);
                throw;
            }
        }

        public async Task<string> SaveImageAsync(string relatPath, string imgUrl)
        {
            var baseDir = "C:\\PlantCollect\\";

            var fileRelatPath = relatPath.Replace("/", "\\").Trim('\\');
            var filePath = Path.Combine(baseDir, fileRelatPath);
            var dir = Path.GetDirectoryName(filePath);


            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var client = new HttpClient();
                var stream = await client.GetStreamAsync(imgUrl);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        if (ms.CanSeek)
                            ms.Seek(0, SeekOrigin.Begin);
                        var buffer = new byte[ms.Length];
                        ms.Read(buffer, 0, buffer.Length);
                        fs.Write(buffer, 0, buffer.Length);
                        Logger.Debug("保存图片:" + filePath);
                    }
                }

                return fileRelatPath;
                //using (var imageFs = System.Drawing.Image.FromStream(stream, false, true))
                //{
                //    imageFs.Save(filePath);

                //}



            }
            catch (Exception ex)
            {
                Logger.Error("Exception:" + ex.Message);
                return string.Empty;
            }

        }
    }
}
