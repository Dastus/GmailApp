using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.IO;
using GmailListApp.Models;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace GmailListApp.Service.Gmail
{
    public static class AuthHelper
    {
        public static string GetOAuth2URL()
        {
            string clientId = Constants.GoogleClientId;
            string redirectUrl = Constants.RedirectURL;
            string scope = "https://www.googleapis.com/auth/gmail.readonly";
            string additional_settings = "approval_prompt=force&access_type=offline";
            return "https://accounts.google.com/o/oauth2/auth?redirect_uri="
                + redirectUrl
                + "&response_type=code&client_id="
                + clientId + "&scope=" + scope + "&" + additional_settings;
        }

        public static void GetAccessToken(string code)
        {
            string google_client_id = Constants.GoogleClientId;
            string google_client_secret = Constants.GoogleClientSecret;
            string google_redirect_url = Constants.RedirectURL;

            /*Get Access Token and Refresh Token*/
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create
              ("https://accounts.google.com/o/oauth2/token");
            webRequest.Method = "POST";
            string parameters = "code=" + code + "&client_id=" + google_client_id +
                      "&client_secret=" + google_client_secret + "&redirect_uri="
                      + google_redirect_url + "&grant_type=authorization_code";
            byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream postStream = webRequest.GetRequestStream();

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();
            WebResponse response = webRequest.GetResponse();
            postStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(postStream);
            string responseFromServer = reader.ReadToEnd();
            GoogleAccessToken accessToken = JsonConvert.DeserializeObject
                <GoogleAccessToken>(responseFromServer);
            /*End*/

            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Add("accessToken", accessToken, DateTime.Now.AddMinutes(30));
        }
    }
}