using System.Web;
using System.Web.Mvc;

namespace GmailClient.WebUI
{
    public static class FilterConfig
    {
        /// <summary>
        /// Register filters one time for all actions
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
