using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Schedule
{
    public interface IRequestMessageProvider
    {
        IEnumerable<RequestMessage> List(int number);
        IEnumerable<RequestMessage> PriorityList(int number);
    }
}
