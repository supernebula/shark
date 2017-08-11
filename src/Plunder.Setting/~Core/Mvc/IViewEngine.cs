using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Plunder.Setting.Core.Mvc
{
    public interface IViewEngine
    {
        ViewEngineResult FindPartialView(HttpControllerContext controllerContext, string partialViewName, bool useCache);
        ViewEngineResult FindView(HttpControllerContext controllerContext, string viewName, string masterName, bool useCache);
        void ReleaseView(HttpControllerContext controllerContext, IView view);
    }
}
