using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Schedule;

namespace Plunder.Plugin.Storage
{
    public class EFRequestMessageProvider : IRequestMessageProvider
    {
        public IEnumerable<RequestMessage> List(int number)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestMessage> PriorityList(int number)
        {
            throw new NotImplementedException();
        }
    }
}
