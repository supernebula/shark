using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class PageResult<T>
    {
        public List<Request> NewRequest { get; set; }

        public T Model { get; set; }
    }
}
