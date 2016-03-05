using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class PageResult<T> where T : new()
    {
        List<Request> NewRequest { get; set; }

        T Model { get; set; }
    }
}
