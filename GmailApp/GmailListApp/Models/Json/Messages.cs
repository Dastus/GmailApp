using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GmailListApp.Models
{
    public class Messages
    {        
        public List<Message> messages { get; set; }
        public string nextPageToken { get; set; }
    }
}