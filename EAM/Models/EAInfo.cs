using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAManager.Models
{
    public class EAInfo:ErrorCodeModel
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public int RequestState { get; set; }
        public DateTime RequestExpires { get; set; }
        public string ResponseValue { get; set; }
        public DateTime ResponseTime { get; set; }
    }
}