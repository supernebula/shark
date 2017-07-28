using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Plunder.Compoment
{
    public class PageResult
    {
        //public string Topic { get; set; }

        public Request Request { get; set; }

        public Response Response { get; set; }

        public IEnumerable<Request> NewRequests { get; set; }

        public string Channel { get; set; }

        public IEnumerable<ResultField> Data { get; set; }

        public static PageResult EmptyResponse(/*string topic, */Request request, Response response, string channel)
        {
            return new PageResult
            {
                //Topic = topic,
                Request = request,
                Response = response,
                Channel = channel
            };

        }
    }


    public class ResultField
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
