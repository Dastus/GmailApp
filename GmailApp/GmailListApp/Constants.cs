using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailListApp
{
    public static class Constants
    {
        public const string URL = "";
        public const string RedirectURL = "http://localhost:53852/";
        //pagination settings
        public const int totalEmailsCount = 100;
        public const int emailsPerPage = 10;               
        //fields below shouldn't be stored as plain text!:
        public const string GoogleClientId = "339671843988-bi6p9iua1i542cnp1lp7ihss6u3nnfan.apps.googleusercontent.com";
        public const string GoogleClientSecret = "W8tKm5lGusWfccD4s2OewxVQ";        
    }
}