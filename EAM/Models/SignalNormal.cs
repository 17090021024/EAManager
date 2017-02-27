using System.Collections.Generic;

namespace EAManager.Models
{
    public class SignalNormal:ErrorCodeModel
    {
        public List<string> Signals { get; set; }

        public int DefaultTpPoint { get; set; }
        public int DefalutSlPoint { get; set; }
        public int DefaultWr { get; set; }
        public string DefalutSource { get; set; }
    }
}