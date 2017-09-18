using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailListApp.Models
{
    public class RootObject
    {
        public string id { get; set; }
        public string threadId { get; set; }
        public List<string> labelIds { get; set; }
        public string snippet { get; set; }
        public string historyId { get; set; }
        public string internalDate { get; set; }
        public Payload payload { get; set; }
        public int sizeEstimate { get; set; }
    }
}