using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NReco.PhantomJS;

namespace PlunderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var phantomJS = new PhantomJS();
            phantomJS.OutputReceived += (sender, e) => {
                Console.WriteLine("PhantomJS output: {0}", e.Data);
            };
            //phantomJS.Run("", new );
            phantomJS.RunScript("for (var i=0; i<10; i++) console.log('hello from js '+i); phantom.exit();", null);
            Console.ReadKey();
        }
    }
}