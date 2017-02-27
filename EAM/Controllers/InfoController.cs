using System;
using System.Web.Mvc;
using EAManager.DBAccess;
using EAManager.Models;
using Common;

namespace EAManager.Controllers
{
    public class InfoController : Controller
    {
        public JsonResultPro SymbolPrice(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return new JsonResultPro { Data = ErrorCodeModel.Error("SymbolNull") };

            EAInfo info = DBA.GetEAInfo(symbol);
            if (info == null)
                return new JsonResultPro { Data = ErrorCodeModel.Error("EAInfoNull") };

            DateTime now = DateTime.Now;

            //汇价距离过期时间还剩一半，主动延长过期时间
            if ((info.RequestExpires - now).TotalSeconds < Config.SymbolPriceRequestExpiresSeconds / 2)
                DBA.RequestEAInfo(symbol, now.AddSeconds(Config.SymbolPriceRequestExpiresSeconds));

            if ((now - info.ResponseTime).TotalSeconds > Config.SymbolPriceAvalSeconds)
                return new JsonResultPro { Data = ErrorCodeModel.Error("EAInfoExpired") };

            string[] tmp = info.ResponseValue.Split(',');
            if (tmp == null || tmp.Length < 2)
            {
                DBA.RequestEAInfo(symbol, now.AddSeconds(Config.SymbolPriceRequestExpiresSeconds));
                return new JsonResultPro { Data = ErrorCodeModel.Error("ResponseValueInvalid") };
            }

            SymbolPrice price = new SymbolPrice();
            price.Ask = tmp[0].ToDouble(-1);
            price.Bid = tmp[1].ToDouble(-1);

            if (price.Ask <= 0 || price.Bid <= 0)
                return new JsonResultPro { Data = ErrorCodeModel.Error("PriceInvalid") };

            return new JsonResultPro() { Data = price };
        }

    }
}
