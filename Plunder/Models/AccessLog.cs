using Evol.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Models
{
    public class AccessLog : IEntity<string>
    {
        public string Id { get; set; }

        public string Domian { get; set; }

        public string Uri { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessCode { get; set; }

        /// <summary>
        /// 耗时毫秒数
        /// </summary>
        public int ElapsedTime { get; set; }

        public DateTime CreateTime { get; set; }


    }
}
