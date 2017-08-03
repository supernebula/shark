using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class Subject
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsMatch(string url)
        {
            throw new NotImplementedException();
        }
    }
}
