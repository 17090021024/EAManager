using Common;
using System;

namespace EAManager.Models
{
    public class Cookies
    {
        public static string DefaultTpPoint
        {
            get { return WebHelper.GetCookie("DefaultTpPoint"); }
            set
            {
                if (value == null) WebHelper.DelCookie("DefaultTpPoint", null, null, false);
                else WebHelper.SetCookie("DefaultTpPoint", value, DateTime.Now.AddYears(1), null, null, false, false);
            }
        }

        public static string DefalutSlPoint
        {
            get { return WebHelper.GetCookie("DefalutSlPoint"); }
            set
            {
                if (value == null) WebHelper.DelCookie("DefalutSlPoint", null, null, false);
                else WebHelper.SetCookie("DefalutSlPoint", value, DateTime.Now.AddYears(1), null, null, false, false);
            }
        }

        public static string DefaultWr
        {
            get { return WebHelper.GetCookie("DefaultWr"); }
            set
            {
                if (value == null) WebHelper.DelCookie("DefaultWr", null, null, false);
                else WebHelper.SetCookie("DefaultWr", value, DateTime.Now.AddYears(1), null, null, false, false);
            }
        }

        public static string DefalutSource
        {
            get { return WebHelper.GetCookie("DefalutSource"); }
            set
            {
                if (value == null) WebHelper.DelCookie("DefalutSource", null, null, false);
                else WebHelper.SetCookie("DefalutSource", value, DateTime.Now.AddYears(1), null, null, false, false);
            }
        }
        
        
    }
}