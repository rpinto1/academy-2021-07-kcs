using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KCSit.SalesforceAcademy.Lasagna.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content,string mail)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = "This is the link to recover the password --> http://www.localhost:3000/recover/" + HttpUtility.UrlEncode(content)+"/"+mail;
        }
    }
}
