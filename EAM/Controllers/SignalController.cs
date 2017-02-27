using System.Web.Mvc;
using EAManager.DBAccess;
using EAManager.Models;
using Common;
using System;

namespace EAManager.Controllers
{
    public class SignalController : Controller
    {
        public ActionResult Normal()
        {
            SignalNormal vm = new SignalNormal();
            vm.Signals = DBA.GetAllSymbol();
            vm.DefaultTpPoint = Cookies.DefaultTpPoint.ToInt(60);
            vm.DefalutSlPoint = Cookies.DefalutSlPoint.ToInt(50);
            vm.DefaultWr = Cookies.DefaultWr.ToInt(50);
            vm.DefalutSource = Cookies.DefalutSource;
            return View(vm);
        }

        public JsonResultPro SubmitNormal(string symbol, int tradeType, string source, int tpPoints, int slPoints, int winningRate, double expectPrice)
        {
            DateTime now = DateTime.Now;

            SignalItem signal = new SignalItem();
            signal.Symbol = symbol;
            signal.ExpectPrice = expectPrice;
            signal.TradeType = tradeType;
            signal.Source = source;
            signal.TpPoints = tpPoints;
            signal.SlPoints = slPoints;
            signal.WinningRate = (double)(winningRate / 100d);
            signal.OutbreakTime = DateTime.MinValue;
            signal.Timeout = -1;
            signal.DistancePoints = -1;
            signal.SignalType = 1;
            signal.State = 1;
            signal.Expires = now.AddSeconds(Config.NormalSignalExpiresSeconds);
            signal.CreateTime = now;
            DBA.InsertSignal(signal);
            DBA.RequestEAInfo("signal", DBA.InitDateTime);

            Cookies.DefalutSlPoint = slPoints.ToString();
            Cookies.DefaultTpPoint = tpPoints.ToString();
            Cookies.DefalutSource = source;
            Cookies.DefaultWr = winningRate.ToString();

            return new JsonResultPro { Data = ErrorCodeModel.Error(string.Empty) };
        }
    }
}
