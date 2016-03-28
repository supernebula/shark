using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Plunder.WebHost
{
    public class ProgramApp
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<ConsoleStartup>("http://localhost:5000"))
            {
                Console.WriteLine("listen in http://localhost:5000");
                Console.WriteLine("Press [Enter] to quit...");
                Console.ReadLine();
            }
        }
    }
}
