using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plunder.Compoment;
using Plunder.Test.Pipeline;

namespace Plunder.Test
{
    [TestClass]
    public class MutliTaskTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Trace.WriteLine("#start statment");
            var pipeline = new ExecutePipeline();
            pipeline.Inject(new PageResult());
            Trace.WriteLine("#end statment");
        }
    }
}
