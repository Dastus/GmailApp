using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GmailListApp.UI.ViewModels;
using GmailListApp.Models;

namespace GmailListApp.Service
{
    public interface IEmailManager
    {
        ViewModel GetViewModel(GoogleAccessToken accessToken, int page);
    }
}
