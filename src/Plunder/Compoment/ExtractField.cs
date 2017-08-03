using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class ExtractField
    {

        public string Key { get; set; }

        public string Title { get; set; }

        public string XPath { get; set; }

        public string CssSelector { get; set; }

        public DataType DataType { get; set; }
    }
}
