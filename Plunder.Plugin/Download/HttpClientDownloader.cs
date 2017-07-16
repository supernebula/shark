
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Download;
using System.Diagnostics;

namespace Plunder.Plugin.Download
{
    public class HttpClientDownloader : IDownloader
    {
        private readonly int _maxTaskNumber;
        private int _currentTaskNumber; 
        //private readonly SemaphoreSlim _ctnLock = new SemaphoreSlim(1);

        public string ContentType => WebPageType.Static;

        public int DownloadingTaskCount
        {
            get { return _currentTaskNumber; }
        }

        public bool IsDefault { get; set; }

        public HttpClientDownloader(int maxTaskNumber)
        {
            _maxTaskNumber = maxTaskNumber;
        }

        [Obsolete]
        public void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> onDownloadComplete, Action onConsumed)
        {
            foreach (Request req in requests)
            {
                // _currentTaskNumber++
                Interlocked.Increment(ref _currentTaskNumber); 
                Task.Run(async () =>
                {
                    try
                    {
                        var client = HttpClientBuilder.GetClient(req.SiteId);
                        Console.WriteLine(@"开始执行Http下载，占位符." + req.Url);
                        var httpResp = await client.GetAsync(req.Url);
                        string content = null;
                        if (httpResp.IsSuccessStatusCode)
                            content = await httpResp.Content.ReadAsStringAsync();
                        Console.WriteLine(@"下载完成:" + req.Url);
                        var resp = new Response()
                        {
                            Request = req,
                            HttpStatusCode = httpResp.StatusCode,
                            IsSuccessCode = httpResp.IsSuccessStatusCode,
                            ReasonPhrase = httpResp.ReasonPhrase,
                            Content = content
                        };
                        return new Tuple<Request, Response>(req, resp);
                    }
                    finally
                    {
                        // _currentTaskNumber--
                        Interlocked.Decrement(ref _currentTaskNumber); 
                    }

                }).ContinueWith(t =>
                {
                    onDownloadComplete(t.Result.Item1, t.Result.Item2);
                }).ContinueWith(t => onConsumed());  
            }
        }

        public async Task<Tuple<Request, Response>> DownloadAsync(Request request)
        {
            try
            {
                //当前任务数加1，原子执行
                Interlocked.Increment(ref _currentTaskNumber);
                var client = HttpClientBuilder.GetClient(request.SiteId);
                var sw = new Stopwatch();
                sw.Start();
                var httpRespMessage = await client.GetAsync(request.Url);
                sw.Stop();
                var elapsed = sw.ElapsedMilliseconds;
                sw = null;
                string content = null;
                Stream stream = null;
                if (httpRespMessage.IsSuccessStatusCode)
                {
                    var contentType = httpRespMessage.Content.Headers.ContentType;
                    content = await httpRespMessage.Content.ReadAsStringAsync();
                    stream = await httpRespMessage.Content.ReadAsStreamAsync();
                }

                var resp = new Response()
                {
                    Request = request,
                    HttpStatusCode = httpRespMessage.StatusCode,
                    IsSuccessCode = httpRespMessage.IsSuccessStatusCode,
                    ReasonPhrase = httpRespMessage.ReasonPhrase,
                    Content = content,
                    StreamContent = stream,
                    ElapsedTime = elapsed
                };
                return new Tuple<Request, Response>(request, resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //当前任务减1，原子执行
                Interlocked.Decrement(ref _currentTaskNumber);
            }
        }

        public bool IsAllowDownload()
        {
            return _maxTaskNumber > _currentTaskNumber;
        }

        public void ReInit()
        {
            throw new NotImplementedException();
        }
    }
}
