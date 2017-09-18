using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GmailListApp.Models
{
    public class Part
    {
        public string partId { get; set; }
        public string mimeType { get; set; }
        public string filename { get; set; }
        public List<Header2> headers { get; set; }
        public Body2 body { get; set; }
    }
}