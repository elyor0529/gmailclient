using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailClient.WebUI.BLL
{
    public static class Settings
    {

        /// <summary>
        /// GAC settngs
        /// </summary>
        public struct CLR
        {
            /// <summary>
            /// Fx dll key
            /// </summary>
            public const string FRAMEWORK_ASSEMBLY = "GmailClient.Framework";
        }

        /// <summary>
        /// generic UI or Back-end key for custom auth
        /// </summary>
        public struct KEYS
        {
            /// <summary>
            /// PSW sessian key
            /// </summary>
            public const string PSW_SESSION_KEY = "_psw_session_key_";

            /// <summary>
            /// Temp data success key
            /// </summary>
            public const string TMP_SUCCESS_KEY = "_tmp_success_key_";
        }

    }
}