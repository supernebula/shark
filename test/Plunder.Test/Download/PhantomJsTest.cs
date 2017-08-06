using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Test.Download
{
    [TestClass]
    public class PhantomJsTest
    {
        [TestMethod]
        public void PhantomJsTest1()
        {
            var url = "https://www.baidu.com/";
            var driver1 = new PhantomJSDriver();
            driver1.Navigate().GoToUrl(url);
            var content = driver1.PageSource;
            driver1.Quit();
            Trace.WriteLine(content);
        }

        private PhantomJSDriverService GetPhantomJSDriverService()
        {
            PhantomJSDriverService pds = PhantomJSDriverService.CreateDefaultService();
            //设置代理服务器地址
            //pds.Proxy = $"{ip}:{port}"; 
            //设置代理服务器认证信息
            //pds.ProxyAuthentication = GetProxyAuthorization();
            return pds;
        }
    }


}
