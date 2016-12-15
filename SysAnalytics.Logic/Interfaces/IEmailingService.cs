using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Web.Core.ViewModels;

namespace SysAnalytics.Logic.Interfaces
{
    public interface IEmailingService
    {
        Task<EmailingInfoDto> GetInfoMessage(string id);
        Task<EmailingInfoDto> SendMessage(string recepient, string fileName, string content);
    }
}
