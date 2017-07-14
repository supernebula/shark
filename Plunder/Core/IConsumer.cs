using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Core
{
    public interface IConsumer
    {
        string Topic { get; }

    }
}
