using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailListApp.Models
{
    public class Payload
    {
        public string partId { get; set; }
        public string mimeType { get; set; }
        public string filename { get; set; }
        public List<Header> headers { get; set; }
        public Body body { get; set; }
        public List<Part> parts { get; set; }
    }
}