using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Diagnostics;

namespace Plunder.Test.Analyze
{
    [TestClass]
    public class SelectorTest
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void ExtractTest()
        {
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync("https://movie.dobn.com/subject/26363254/comments").GetAwaiter().GetResult();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var h1Node = doc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/h1");
            var commentNodes = doc.DocumentNode.SelectNodes("//*[@id=\"comments\"]/div");
            foreach (var node in commentNodes)
            {
                var avatar = node.SelectSingleNode("div[1]/a/img");
                var nick = node.SelectSingleNode("div[2]/h3/span[2]/a");
                var rating = node.SelectSingleNode("div[2]/h3/span[2]/span[2]");
                var time = node.SelectSingleNode("div[2]/h3/span[2]/span[3]");
                var agreen = node.SelectSingleNode("div[2]/h3/span[1]/span");
                var comment = node.SelectSingleNode("div[2]/p");


                //*[@id="comments"]/div[1]/div[2]/h3/span[2]/a
                //*[@id="comments"]/div[2]/div[2]/h3/span[2]/a
                //*[@id="comments"]/div[3]/div[2]/h3/span[2]/a
                //*[@id="comments"]/div[4]/div[2]/h3/span[2]/a



                if (avatar == null && nick == null)
                    break;

                var src = avatar?.GetAttributeValue("src", "");
                Trace.WriteLine($"avatar:{src}");
                Trace.WriteLine($"nick:{nick?.InnerText?.Trim()}");
                Trace.WriteLine($"rating:{rating?.InnerText?.Trim()}");
                Trace.WriteLine($"time:{time?.InnerText?.Trim()}");
                Trace.WriteLine($"agreen:{agreen?.InnerText?.Trim()}");
                Trace.WriteLine($"comment:{comment?.InnerText?.Trim()}");
            }

            httpClient.Dispose();
        }
    }
}
