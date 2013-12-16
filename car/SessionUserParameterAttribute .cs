using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace car
{
    public class SessionUserParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user=HttpContext.Current.Session["user"];
            if (user != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/home/login");
            }
            /*const string key = "sessionUser";

            if (filterContext.ActionParameters.ContainsKey(key))
            {
                filterContext.ActionParameters[key] = HttpContext.Current.Session["UserAccuont"];//为Action设置参数
            }
*/
           
        }
    }
}