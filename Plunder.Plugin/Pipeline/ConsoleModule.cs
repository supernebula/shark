using System;
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
        private int _cursorLeft;
        private int _cursorTop;
        private int _bufferHeight;
        private int _bufferWidth;
        private bool _isNewBuffer;
        private bool _isBufferFixedHeight;
        public ConsoleModule( int cursorLeft, int cursorTop, int bufferHeight, int bufferWidth, bool isNewBuffer, bool isBufferFixedHeight)
        {
            _cursorLeft = cursorLeft;
            _cursorTop = cursorTop;
            _bufferHeight = bufferHeight;
            _bufferWidth = bufferWidth;
            _isNewBuffer = isNewBuffer;
            _isBufferFixedHeight = isBufferFixedHeight;
    }

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

        public Task ProcessAsync(PageResult data)
        {
            return Execute(data);
        }

        private async Task Execute(PageResult data)
        {
            await Task.Run(() =>
            {
                WriteLine();
            });
        }

        private void WriteLine()
        {
            Console.WriteLine("");
        }
    }
}
