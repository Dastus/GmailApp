﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailListApp.Models
{
    public class GoogleAccessToken
    {
        public string access_token;
        public string token_type;
        public int expires_in;
        public string refresh_token;
    }
}