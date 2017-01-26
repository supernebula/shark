using System;
using System.Linq;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Plugin.Pipeline
{
    public class ConsoleModule : IPipelineModule
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
            Console.CursorLeft = _cursorLeft;
            Console.CursorTop = _cursorTop;
            //Console.BufferHeight = _bufferHeight;
            //Console.BufferWidth = _bufferWidth;
            Console.SetBufferSize(_bufferWidth, _bufferWidth);
        }

        public string Name
        {
            get
            {
                return "控制台模块";
            }
        }

        public string Description
        {
            get
            {
                return "输出信息到控制台";
            }
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
                var newReqCount = data.NewRequests?.Count() ?? 0;
                WriteLine($"Url:{data.Request.Url}, StatusCode:{data.Response.HttpStatusCode}, New Request Count:{newReqCount}");
            });
        }

        private void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
    }
}
