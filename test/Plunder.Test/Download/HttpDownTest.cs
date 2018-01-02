using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plunder.Test.Download
{
    [TestClass]
    public class HttpDownTest
    {
        [TestMethod]
        public void ByHeaderTest()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Referrer = new Uri("http://www.plantphoto.cn/tu/3009477");

            var stream = client.GetStreamAsync("http://img.plantphoto.cn/image2/b/3009477.jpg").Result;

            //var ms = new MemoryStream();
            //stream.CopyTo(ms);

            //// 把 Stream 转换成 byte[]
            //byte[] bytes = new byte[ms.Length];
            //ms.Read(bytes, 0, bytes.Length);
            //// 设置当前流的位置为流的开始
            //if(ms.CanSeek)
            //    ms.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            var dir = "C:\\plunder";
            if (Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var ImageFs = System.Drawing.Image.FromStream(stream);
            ImageFs.Save(Path.Combine(dir, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_3009477.jpg"));

            //var fs = new FileStream(Path.Combine(dir, "3009477.jpg"), FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);
            //bw.Write(bytes);
            //bw.Close();
            //fs.Close();
        }
    }
}
