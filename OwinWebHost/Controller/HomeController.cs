using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace Plunder.WebHost.Controller
{
    public class HomeController : ApiController 
    {
        //GET api/default
        [HttpGet]
        public IHttpActionResult Default()
        {
            return Json(new {Name = "Hello", First = "World"});
        }
    }
}
