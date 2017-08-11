using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Plunder.Setting.Core.Mvc
{
    public class ViewResult : IHttpActionResult
    {
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
