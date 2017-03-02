using System.Configuration;
using EACommon;

namespace EAManager.Models
{
    public class Config
    {
        static Config()
        {
            SymbolPriceAvalSeconds = ConfigurationManager.AppSettings["SymbolPriceAvalSeconds"].ToInt();
            SymbolPriceRequestExpiresSeconds = ConfigurationManager.AppSettings["SymbolPriceRequestExpiresSeconds"].ToInt();
            NormalSignalExpiresSeconds = ConfigurationManager.AppSettings["NormalSignalExpiresSeconds"].ToInt();
        }

        public static int SymbolPriceAvalSeconds { get; set; }
        public static int SymbolPriceRequestExpiresSeconds { get; set; }
        public static int NormalSignalExpiresSeconds { get; set; }

    }
}