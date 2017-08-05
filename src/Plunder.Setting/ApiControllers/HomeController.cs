using System.Web.Http;

namespace Plunder.Setting.ApiControllers
{
    public class HomeController : ApiController
    {
        public string Index()
        {
            return "This is Home.Index";
        }
    }
}
