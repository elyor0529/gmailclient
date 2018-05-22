using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GmailClient.WebUI.BLL.Attributes
{
    /// <summary>
    /// Check password if not yet in Session storage
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CheckPasswordAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.RequestContext.HttpContext;

            //chech auth
            if (context.Request.IsAuthenticated)
            {
                //after login we set Gmail password
                var psw = (string)context.Session[Settings.KEYS.PSW_SESSION_KEY];

                //if psw has we returned asking page
                if (!String.IsNullOrWhiteSpace(psw))
                    return;

                //abandon all auth resources(cookie,session,...)
                context.GetOwinContext().Authentication.SignOut();

                //redirect to auth page with default redict url: by defalut ~/Account/Login maybe
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}