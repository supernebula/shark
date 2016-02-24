using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;

namespace Plunder.Test
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            BlockingCollection<object> bc = new BlockingCollection<object>();
        }
    }
}
