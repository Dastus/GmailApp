using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GmailListApp.Service.Gmail;
using GmailListApp.UI.ViewModels;
using GmailListApp.Service;
using System.Runtime.Caching;
using GmailListApp.Models;

namespace GmailListApp.Controllers
{
    public class HomeController : Controller
    {

        private IEmailManager emailManager;
        MemoryCache memoryCache = MemoryCache.Default;

        public HomeController(IEmailManager emailManager)
        {
            this.emailManager = emailManager;
        }

        // GET: Home
        public ActionResult Index()
        {            
            var accessToken = memoryCache.Get("accessToken") as GoogleAccessToken;

            if (accessToken == null)
            {
                string url = AuthHelper.GetOAuth2URL();
                try
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["code"]))
                        AuthHelper.GetAccessToken(Request.QueryString["code"]);
                    else
                        return Redirect(url);
                }
                catch (System.Net.WebException)
                {
                    ViewBag.WebExceptionTriggered = true;
                }                   
            }

            return View();
        }

        public ActionResult GetMessages(int pageId)
        {
            try
            {
                var accessToken = memoryCache.Get("accessToken") as GoogleAccessToken;
                ViewModel vm = emailManager.GetViewModel(accessToken, pageId);
                return PartialView("_Messages", vm);
            }
            catch (System.Net.WebException)
            {
                ViewBag.WebExceptionTriggered = true;                
            }
            return PartialView("_Messages");
        }

        public ActionResult Logout()
        {
            memoryCache.Remove("accessToken");
            return RedirectToAction("Index", "Home");
        }
    }
}