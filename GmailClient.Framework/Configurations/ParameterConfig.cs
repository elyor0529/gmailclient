using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailClient.Framework.Configurations
{
    public struct ParameterConfig
    {
        /// <summary>
        /// client credential user name param key for IOC ctor
        /// </summary>
        public const string CREDENTIALS_USR_KEY = "usr";

        /// <summary>
        /// client credential password param key for IOC ctor
        /// </summary>
        public const string CREDENTIALS_PSW_KEY = "psw";
    }
}
