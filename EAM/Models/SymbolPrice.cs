using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAManager.Models
{
    public class SymbolPrice:ErrorCodeModel
    {
        public double Ask { get; set; }
        public double Bid { get; set; }
    }
}