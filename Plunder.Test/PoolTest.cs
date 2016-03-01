using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Diagnostics;

namespace Plunder.Test
{
    [TestClass]
    public class PoolTest
    {
        [TestMethod]
        public void ThreadPoolTest()
        {
            Trace.WriteLine(String.Format("CLR Version: {0}", Environment.Version));

            int wThreads, cpThreads;
            ThreadPool.GetMaxThreads(out wThreads, out cpThreads); 
            //ThreadPool.UnsafeQueueUserWorkItem();


            Trace.WriteLine(String.Format("workThread Number: {0}, completionPortThread Number:{1}", wThreads, cpThreads));
        }
    }
}
