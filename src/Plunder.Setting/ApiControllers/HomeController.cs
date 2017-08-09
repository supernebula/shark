using System.Web.Http;

namespace Plunder.Setting.ApiControllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        public string Index()
        {
            return "This is Home.Index";
        }
    }
}
