using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;


namespace Plunder.Compoment
{
    [DataContract]
    public class Site
    {
        #region Property

        [DataMember]
        public string Id { get; set; }

        public static Site NewDefault => DefaultSite();

        public string Topic { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IndexUrl { get; set; }

        [DataMember]
        public string Domain { get; set; }

        [DataMember]
        public string UserAgent { get; set; }


        public List<KeyValuePair<string, string>> DefaultCookie { get; set; }

        public List<KeyValuePair<string, string>> Cookies { get; set; }

        [DataMember]
        public string Charset { get; set; }

        [DataMember]
        public List<Request> StartRequests { get; set; }

        [DataMember]
        public int SleepMilliseconds { get; set; }

        [DataMember]
        public int RetryTimes { get; set; }

        [DataMember]
        public int AllowedRetryCount { get; set; }

        [DataMember]
        public int CycleRetryTimes { get; set; }

        [DataMember]
        public int RetrySleepMilliseconds { get; set; }

        [DataMember]
        public int TimeOut { get; set; }

        [DataMember]
        public List<HttpStatusCode> AcceptHttpStatCode { get; set; }

        [DataMember]
        public Dictionary<string, string> Headers { get; set; }

        [DataMember]
        public bool EnableHttpProxy { get; set; }

        [DataMember]
        public bool UseGzip { get; set; }

        #endregion

        public Site()
        {

        }

        static Site DefaultSite()
        {
            return new Site()
            {
                AcceptHttpStatCode = new List<HttpStatusCode> { HttpStatusCode.OK }
            };
        }

        public void AddCookie(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void AddCookie(string domain, string name, string value)
        {
            throw new NotImplementedException();
        }


    }
}
