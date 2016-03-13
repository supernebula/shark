using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    [Obsolete]
    public abstract class AbstractConsumerBak : IConsumerBak
    {
        public Guid Id { get; set; }

        public int IdleGeneration { get; set; }

        public bool IsBusy { get; set; }


        public string Topic { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
