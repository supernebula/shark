using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Setting.Core.Mvc
{
    internal class ViewLocation
    {
        protected string _virtualPathFormatString;

        public ViewLocation(string virtualPathFormatString)
        {
            _virtualPathFormatString = virtualPathFormatString;
        }

        public virtual string Format(string viewName, string controllerName)
        {
            return String.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName);
        }
    }
}
