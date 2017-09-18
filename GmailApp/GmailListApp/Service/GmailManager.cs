using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using GmailListApp.UI.ViewModels;
using GmailListApp.Service.Gmail;
using GmailListApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace GmailListApp.Service
{
    public class GmailManager : IEmailManager
    {
        public ViewModel GetViewModel(GoogleAccessToken accessToken, int page)
        {
            var messagesList = GetMessages(accessToken, page);
            List<EmailViewModel> emailsVM = new List<EmailViewModel>(); 

            Parallel.ForEach(messagesList.messages, (message) =>
            {
                using (var client = new WebClient())
                {
                    string url = "https://www.googleapis.com/gmail/v1/users/me/messages/" + message.id;
                    string result = PerformGetRequest(url, accessToken.access_token);
                    var currentMessage = JsonConvert.DeserializeObject<RootObject>(result);
                    EmailViewModel emailInfo = new EmailViewModel { Id = message.id };
                    emailInfo.Subject = currentMessage.payload.headers.Where(e => e.name == "Subject").FirstOrDefault().value;
                    emailsVM.Add(emailInfo);
                }
            });

            ViewModel vm = new ViewModel
            {
                Emails = emailsVM.OrderByDescending(e => e.Id).ToList(),
                PagesCount = (int)Math.Ceiling((double)(Constants.totalEmailsCount / Constants.emailsPerPage))
            };

            return vm;
        }

        private Messages GetMessages(GoogleAccessToken token, int page)
        {
            string refreshToken = token.refresh_token;
            string accessToken = token.access_token;
            string scope = "https://www.googleapis.com/gmail/v1/users/me/messages";            
            string url = scope + "?maxResults=" + Constants.emailsPerPage;

            if (page > 1)
            {
                url += "&pageToken=" + GetPageToken(page, accessToken);
            }                

            Messages messages = new Messages();
            string response = PerformGetRequest(url, accessToken);
            messages = JsonConvert.DeserializeObject<Messages>(response);
            return messages;
        }

        private string GetPageToken(int pageNum, string accessToken)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            string pageToken = memoryCache.Get(pageNum.ToString()) as string;

            if (pageToken != null)
                return pageToken;
            else
                GetAndSaveTokens(accessToken);

            return memoryCache.Get(pageNum.ToString()) as string;            
        }

        void GetAndSaveTokens(string accessToken)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            int pageCount = (int)Math.Ceiling((double)(Constants.totalEmailsCount / Constants.emailsPerPage));
            for (int i = 1; i <= pageCount; i++ )
            {
                string url = "https://www.googleapis.com/gmail/v1/users/me/messages";
                url += "?maxResults=" + Constants.emailsPerPage;
                if (i > 1)
                {
                    url += "&pageToken=" + memoryCache.Get(i.ToString());
                }
                string response = PerformGetRequest(url, accessToken);
                string pageToken = JsonConvert.DeserializeObject<Messages>(response).nextPageToken;
                memoryCache.Add((i + 1).ToString(), pageToken, DateTime.Now.AddMinutes(5));
            }
        }

        string PerformGetRequest(string url, string accessToken)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            webRequest.ContentType = "application/json";

            using (Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    string jsonResponse = sr.ReadToEnd();
                    return jsonResponse;
                }
            }
        }

    }
}