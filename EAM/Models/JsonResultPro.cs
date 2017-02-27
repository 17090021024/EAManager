using System.Web.Mvc;

namespace EAManager.Models
{
    public class JsonResultPro : JsonResult
    {
        public JsonResultPro()
        {
            this.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
        }
    }
}