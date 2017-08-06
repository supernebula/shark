using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plunder.Test.Download
{
    [TestClass]
    public class PhantomJsTest
    {
        [TestMethod]
        public void PhantomJsTest1()
        {
            var watch = new Stopwatch();
            watch.Start();
            var url = "https://www.baidu.com/";
            var driver1 = new PhantomJSDriver();
            driver1.Manage().Window.Size = new Size(1920, 1080);
            driver1.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver1.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
            driver1.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            //driver1.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("name", "value"));

            driver1.Navigate().GoToUrl(url);
            var content = driver1.PageSource;
            watch.Stop();
            Trace.WriteLine($"Elapsed 毫秒:{watch.Elapsed.Milliseconds }");
            driver1.Quit();
            Trace.WriteLine(content);
        }

        [TestMethod]
        public void ScreenshotTest()
        {
            var url = "http://www.bing.com";
            var driver2 = new PhantomJSDriver();
            driver2.Manage().Window.Size = new Size(1440, 900);
            driver2.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver2.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
            driver2.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver2.Navigate().GoToUrl(url);

            for (int i = 0; i < 20; i++)
            {
                driver2.Keyboard.PressKey(Keys.PageDown);
                driver2.Keyboard.ReleaseKey(Keys.PageDown);
            }

            driver2.Keyboard.PressKey(Keys.End);
            driver2.Keyboard.ReleaseKey(Keys.End);

            driver2.Keyboard.PressKey(Keys.Home);
            driver2.Keyboard.ReleaseKey(Keys.Home);

            //for (int i = 0; i < 20; i++)
            //{
            //    driver2.Keyboard.PressKey(Keys.PageDown);
            //    driver2.Keyboard.ReleaseKey(Keys.PageDown);
            //    Thread.Sleep(1000 + i * 100);
            //}

            //driver2.Keyboard.PressKey(Keys.End);
            Thread.Sleep(5000);
            //driver2.Keyboard.PressKey(Keys.Home);


            //driver2.ExecuteScript(scrollJs);

            var screenshotDriver = driver2 as ITakesScreenshot;
            var screenshot = screenshotDriver.GetScreenshot();

            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screenshot", DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, DateTime.Now.ToString("yyyyMMdd-hhmmss-fff") + ".jpg");
            using (MemoryStream stream = new MemoryStream(screenshot.AsByteArray))
            {
                using (var soourceImage = Image.FromStream(stream))
                {
                    try
                    {
                        soourceImage.Save(path, ImageFormat.Jpeg);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    
                }
            }

            Trace.WriteLine(path);
            driver2.Quit();
        }


        private PhantomJSDriverService GetPhantomJSDriverService()
        {
            PhantomJSDriverService pds = PhantomJSDriverService.CreateDefaultService();
            pds.LoadImages = false;
            //设置代理服务器地址
            //pds.Proxy = $"{ip}:{port}"; 
            //设置代理服务器认证信息
            //pds.ProxyAuthentication = GetProxyAuthorization();
            return pds;
        }
    }


}
