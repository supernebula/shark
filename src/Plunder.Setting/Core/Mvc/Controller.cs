using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Plunder.Setting.Core.Mvc
{
    public class Controller : ApiController
    {
        private ExpandoObject _viewDataDictionary;
        public dynamic ViewData
        {
            get
            {
                if (_viewDataDictionary == null)
                {
                    _viewDataDictionary = new ExpandoObject();
                }
                return _viewDataDictionary;
            }
            set { _viewDataDictionary = value; }
        }

        protected internal ViewResult View()
        {
            return View(viewName: null, masterName: null, model: null);
        }

        protected internal ViewResult View(object model)
        {
            return View(null /* viewName */, null /* masterName */, model);
        }

        protected internal ViewResult View(string viewName)
        {
            return View(viewName, masterName: null, model: null);
        }

        protected internal ViewResult View(string viewName, string masterName)
        {
            return View(viewName, masterName, null /* model */);
        }

        protected internal ViewResult View(string viewName, object model)
        {
            return View(viewName, null /* masterName */, model);
        }

        protected internal virtual ViewResult View(string viewName, string masterName, object model)
        {
            if (model != null)
            {
                ViewData.Model = model;
            }

            return new ViewResult
            {
                ViewName = viewName,
                MasterName = masterName,
                ViewData = ViewData,
                TempData = TempData,
                ViewEngineCollection = ViewEngineCollection
            };
        }
    }
}
