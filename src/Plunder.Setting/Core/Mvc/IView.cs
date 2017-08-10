using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Plunder.Setting.Core.Mvc
{
    public interface IView
    {
        void Render(HttpControllerContext controllerContext, TextWriter writer);
    }
}
