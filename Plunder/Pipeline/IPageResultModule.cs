using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Pipeline
{
    public interface IPageResultModule : IDisposable
    {
        string ModuleName { get; }

        string ModuleDescription { get; }

        void Init(object context);

        void Process(IPageResult result);

    }
}
