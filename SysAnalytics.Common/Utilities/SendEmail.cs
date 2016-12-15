using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using DocumentFormat.OpenXml.Wordprocessing;
using Ionic.Zip;

namespace SysAnalytics.Common.Utilities
{
    public class Email
    {
        public static bool SendZipEmail(string recepient, string filePath, string fileName, string content)
        {
            try
            {
                ContentType ct = new ContentType("application/zip");
                var zipFile = filePath + fileName.Replace("csv", "zip");
                if (File.Exists(zipFile)) File.Delete(zipFile);
                
                var file = filePath + fileName;
                if (File.Exists(file)) File.Delete(file);

                File.WriteAllText(file, content);

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(file, string.Empty);
                    zip.Save(zipFile);
                }

                using (MailMessage mail = new MailMessage())
                {
                    string from = WebConfigurationManager.AppSettings["MailServerFrom"];
                    string user = WebConfigurationManager.AppSettings["MailServerUserName"];
                    string key = WebConfigurationManager.AppSettings["MailServerKey"];
                    string host = WebConfigurationManager.AppSettings["MailServerHostAddress"];
                    mail.From = new MailAddress(from);

                    // The important part -- configuring the SMTP client
                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
                    smtp.UseDefaultCredentials = false; // [3] Changed this
                    smtp.Credentials = new NetworkCredential(user, key);  // [4] Added this. Note, first parameter is NOT string.
                    smtp.Host = host;

                    //recipient address
                    mail.To.Add(new MailAddress(recepient));

                    //Formatted mail body
                    mail.IsBodyHtml = true;
                    string st = "Report";

                    mail.Body = st + " from The Sys Analytics";
                    mail.Subject = st;

                    Attachment attachment = new Attachment(zipFile, ct);
                    mail.Attachments.Add(attachment);

                    //foreach (var attachment in attachments)
                    //{
                    //    var lMailAttach = Attachment.CreateAttachmentFromString(attachment.Value, attachment.Key);
                    //    lMailAttach.ContentDisposition.FileName = attachment.Key;
                    //    lMailAttach.ContentDisposition.Inline = false;
                    //    lMailAttach.ContentDisposition.Size = attachment.Value.Length;
                    //    mail.Attachments.Add(lMailAttach);
                    //}
                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (SmtpException ex)
                    {
                        SmtpStatusCode statusCode = ex.StatusCode;
                        return false;
                    }
                }

            }
            catch (Exception exception)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}
