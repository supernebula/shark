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


            var dir = "C:\\plunder";
            if (Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var ImageFs = System.Drawing.Image.FromStream(stream);
            ImageFs.Save(Path.Combine(dir, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_3009477.jpg"));

        }

        [TestMethod]
        public void PlantCsdbThumbDownTest()
        {
            var client = new HttpClient();
            var stream = client.GetStreamAsync("http://www.cfh.ac.cn/Data/2009/200911/20091110/Thumbnail/a4b5c793-cfec-4265-863d-b18820b7e652.jpg").Result;


            var dir = "C:\\plunder";
            if (Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var ImageFs = System.Drawing.Image.FromStream(ms);
                ImageFs.Save(Path.Combine(dir, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_3009477.jpg"));
            }

        }

        [TestMethod]
        public void BytesPlantCsdbThumbDownTest()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Referrer = new Uri("http://www.plantphoto.cn/tu/3009477");

            var bytes = client.GetAsync("http://www.cfh.ac.cn/Data/2009/200911/20091110/Thumbnail/a4b5c793-cfec-4265-863d-b18820b7e652.jpg").Result;



            var dir = "C:\\plunder";
            if (Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //using (var ms = new MemoryStream())
            //{
            //    ms.Write(bytes, 0, bytes.Length);
            //    var ImageFs = System.Drawing.Image.FromStream(ms);
            //    ImageFs.Save(Path.Combine(dir, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_3009477.jpg"));
            //}

        }
    }
}
