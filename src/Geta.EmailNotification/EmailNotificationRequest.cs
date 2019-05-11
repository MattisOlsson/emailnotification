using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;
using Geta.EmailNotification.Shared;

namespace Geta.EmailNotification
{
    public class EmailNotificationRequest : EmailNotificationRequestBase
    {
        public EmailNotificationRequest()
        {
            this.Attachments = new List<Attachment>();
            this.To = new MailAddressCollection();
            this.Cc = new MailAddressCollection();
            this.Bcc = new MailAddressCollection();
            this.ReplyTo = new MailAddressCollection();
            this.ViewData = new ViewDataDictionary(this);
        }

        public ViewDataDictionary ViewData { get; set; }
    }
}