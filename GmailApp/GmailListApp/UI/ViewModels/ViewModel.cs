using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailListApp.UI.ViewModels
{
    public class ViewModel
    {
        public List<EmailViewModel> Emails { get; set; }
        public int PagesCount { get; set; } 
    }
}