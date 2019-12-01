using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Helpers
{
    /// <summary>
    /// Provides email services.
    /// </summary>
    public class NotificationService
    {
        public static MailAddress FromAddress { get => new MailAddress(_fromAcct); }
        public string Subject { get; set; }
        public string Body { get; set; }

        private static string _fromAcct = "clydes.takeout.notifications@gmail.com";
        private static string _fromAcctPwd = "ClydeIsAC00lC@t";
        private SmtpClient _smtp;

        public NotificationService(string subject, string body)
        {
            _smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromAddress.Address, _fromAcctPwd)
            };
            Subject = subject;
            Body = body;
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the specified recipient address. 
        /// </summary>
        /// <param name="toAddress">Recipient's email</param>
        /// <param name="subject">Subject text</param>
        /// <param name="body">Body text</param>
        /// <returns>A NotificationService instance.</returns>
        public static NotificationService SendEmail(string toAddress, string subject = null, string body = null)
        {
            NotificationService ns = new NotificationService(subject, body);
            ns.SendEmail(toAddress);
            return ns;
        }

        /// <summary>
        /// Sends an email to the specified recipient with the default subject and body.
        /// </summary>
        /// <param name="toAddress">Recipient's email</param>
        /// <returns>True if email was sent, otherwise false</returns>
        public bool SendEmail(string toAddress)
        {
            try
            {
                if (Subject == null)
                    Subject = "Clydes Takeout Notification";

                if (Body == null)
                    Body = "$lt;notification$gt;";

                using (var emailMessage = new MailMessage(FromAddress, new MailAddress(toAddress))
                {
                    Subject = this.Subject,
                    Body = this.Body
                })
                {
                    _smtp.Send(emailMessage);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
