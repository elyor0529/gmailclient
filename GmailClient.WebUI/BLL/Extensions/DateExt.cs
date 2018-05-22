using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GmailClient.WebUI.BLL.Extensions
{
    public static class DateExt
    {

        /// <summary>
        /// Gmail date and time short format 
        /// </summary>
        /// <param name="dateTime">date time</param>
        /// <returns></returns>
        public static string ToEmailDate(this DateTime? dateTime)
        {
            if (dateTime == null)
                return String.Empty;

            if (dateTime.Value.Date == DateTime.Today)
            {
                return dateTime.Value.ToShortTimeString();
            }
            else
            {
                return dateTime.Value.ToShortDateString();
            }
        }

    }
}