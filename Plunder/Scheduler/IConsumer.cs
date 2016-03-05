using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IConsumer : IDisposable
    {
        Guid Id { get; set; }
        string Topic { get; }

        int IdleGeneration { get; set; }

        bool IsBusy { get; set; }
    }


    public static class IdleGeneration
    {
        public const int ACTIVE = 0;

        public const int JUST_FINISHED = 1;

        public const int IDLE = 3;

        public const int VERY_IDLE = 6;
    }
}
