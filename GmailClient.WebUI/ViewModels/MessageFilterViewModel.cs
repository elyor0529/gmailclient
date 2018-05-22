using GmailClient.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GmailClient.WebUI.ViewModels
{
    public class MessageFilterViewModel
    {

        public string Mailbox { get; set; }

        [Required]
        [DisplayName("Last:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastDate { get; set; }

        [DisplayName("Keyword:")]
        public string Keyword { get; set; }

        public MessageFilterViewModel()
        {
            LastDate = DateTime.Now.AddMonths(-1).Date;
        }

    }
}