using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using SysAnalytics.Common.Logging;
using SysAnalytics.Logic.Interfaces;
using SysAnalytics.Web.Core.ViewModels;
using Ionic.Zip;

namespace SysAnalytics.Logic.Services
{
    public class EmailingService : IEmailingService
    {
        private readonly ILogger _log;

        public EmailingService(ILogger log)
        {
            _log = log;
        }

        public async Task<EmailingInfoDto> GetInfoMessage(string id)
        {
            EmailingInfoDto result = null;
            MandrillApi api = new MandrillApi(WebConfigurationManager.AppSettings["MailServerKey"]);
            try
            {
                var info = await api.GetInfo(new MessageInfoRequest(id));
                return new EmailingInfoDto()
                {
                    EmailId = info.Id,
                    State = info.State.ToString(),
                    Opens = info.Opens,
                    Clicks = info.Clicks
                };
                //var etc = await api.GetContent(new ContentRequest(id));

            }
            catch (Exception ex)
            {
                string err = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                //throw new WebException(err);
            }

            return result;
        }

        public async Task<EmailingInfoDto> SendMessage(string recepient, string file, string content)
        {
            EmailingInfoDto result = new EmailingInfoDto();

            try
            {
                _log.Info("Emailing start");
                
                string from = WebConfigurationManager.AppSettings["MailServerFrom"];
                string key = WebConfigurationManager.AppSettings["MailServerKey"];
            
                var zipFile = file.Replace("csv", "zip");
                //var zipFileName = fileName.Replace("csv", "zip");
                if (File.Exists(zipFile)) File.Delete(zipFile);

                //var file = filePath + fileName;
                if (File.Exists(file)) File.Delete(file);

                File.WriteAllText(file, content);
                

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(file, string.Empty);
                    zip.Save(zipFile);
                }
                MandrillApi api = new MandrillApi(key);
            
                var to = new List<EmailAddress>();
                to.Add(new EmailAddress(recepient));
                var zipBytes = File.ReadAllBytes(zipFile);
                var zipFileName = Path.GetFileName(zipFile);

                var attachments = new List<EmailAttachment>
                {
                    new EmailAttachment
                    {
                        Content = Convert.ToBase64String(zipBytes),
                        Name = zipFileName,
                        Type = "application/zip",
                    },
                };
                var mess = new EmailMessage()
                {
                    To = to , 
                    FromEmail = from,
                    Attachments = attachments.AsEnumerable(),
                    Subject = "Report",
                    Text = "Report from The Sys Analytics."
                };

                SendMessageRequest request = new SendMessageRequest(mess);


                var resp = await api.SendMessage(request);
                var emailResult = resp.FirstOrDefault();
                if (emailResult != null)
                {
                    result.RejectReason = emailResult.RejectReason;
                    result.State = emailResult.Status.ToString();
                }

                _log.Info("Emailing end");

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw new Exception(ex.Message);
            }


            return result;
        }
    }
}
