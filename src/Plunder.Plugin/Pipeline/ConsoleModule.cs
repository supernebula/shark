﻿using Plunder.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using NLog;

namespace Plunder.Plugin.Pipeline
{
    public class ConsoleResultModule : IResultPipelineModule
    {
        private ILogger Logger = LogManager.GetLogger("result");

        public string Description => "简单控制台结果中间件";

        public string Name => "简单控制台结果中间件";

        public void Init(object context)
        {
        }

        public Task ProcessAsync(PageResult result)
        {
            Console.WriteLine($"{result.Request.Url}");
            if(result.Data != null)
                ShowResultField(result.Data);
            return Task.FromResult(1);
        }

        private void ShowResultField(IEnumerable<ResultField> data)
        {
            foreach (var field in data)
            {
                Logger.Info($"{field.Name}:{field.Value}");
                //Console.WriteLine($"{field.Name}:{field.Value}");
            }
        }
    }
}
