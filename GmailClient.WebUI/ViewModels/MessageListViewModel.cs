using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailClient.WebUI.ViewModels
{
    public class MessageListViewModel
    {
        public IEnumerable<MessageViewModel> Items { get; set; }

        public MessageFilterViewModel Filter { get; set; }

    }
}