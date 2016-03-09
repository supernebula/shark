﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Plugin.Pipeline
{
    public class ConsoleModule : IResultPipelineModule
    {
        public string ModuleName
        {
            get
            {
                return "控制台模块";
            }
        }

        public string ModuleDescription
        {
            get
            {
                return "输出信息到控制台";
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(object context)
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync<T>(PageResult<T> data)
        {
            return Execute(data);
        }

        private async Task Execute<T>(PageResult<T> data)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("");
            });
        }
    }
}
