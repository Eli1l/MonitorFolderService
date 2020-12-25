using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailService
{
    public class Message
    {
        private string[] vs;
        private string v;
        private string fullPath;

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }
        public Message(IEnumerable <string> to, string subject, string content, string filepath)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachment = filepath;
        }

        public Message(string[] vs, string v, string fullPath)
        {
            this.vs = vs;
            this.v = v;
            this.fullPath = fullPath;
        }
    }
}
