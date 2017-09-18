using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GmailListApp.Models
{
    public class Message
    {        
        public string id { get; set; }        
        public string threadId { get; set; }
    }
}