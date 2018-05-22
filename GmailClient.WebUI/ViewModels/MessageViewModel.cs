using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GmailClient.WebUI.ViewModels
{
    public class MessageViewModel
    {

        [ScaffoldColumn(false)]
        public uint UId { get; set; }

        [Required]
        [StringLength(250)]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [AllowHtml]
        [StringLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public string From { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string To { get; set; }

        public bool Readed { get; set; }

        public string Date { get; set; }

    }
}