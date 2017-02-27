using System;

namespace EAManager.Models
{
    public class SignalItem : ErrorCodeModel
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public int TradeType { get; set; }
        public string Source { get; set; }
        public int TpPoints { get; set; }
        public int SlPoints { get; set; }
        public double WinningRate { get; set; }
        public DateTime OutbreakTime { get; set; }
        public int Timeout { get; set; }
        public int DistancePoints { get; set; }
        public int SignalType { get; set; }
        public int State { get; set; }
        public DateTime Expires { get; set; }
        public DateTime CreateTime { get; set; }
        public double ExpectPrice { get; set; }

    }
}