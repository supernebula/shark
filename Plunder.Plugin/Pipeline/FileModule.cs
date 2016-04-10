using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;
using log4net;

namespace Plunder.Plugin.Pipeline
{
    public class FileModule : IResultPipelineModule
    {
        public string ModuleName
        {
            get
            {
                return "文件存储模块";
            }
        }

        public string ModuleDescription
        {
            get
            {
                return "数据持久化到文件";
            }
        }

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync(PageResult data)
        {
            await Task.Run(() =>
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                File.AppendAllLines(baseDir + "page_result.txt", new List<string>() { String.Format("Url:{0}, StatusCode:{1}, New Request Count:{2}", data.Request.Uri, data.Response.HttpStatusCode, data.NewRequests.Count()) }, Encoding.UTF8);
            });
            
        }
    }
}
