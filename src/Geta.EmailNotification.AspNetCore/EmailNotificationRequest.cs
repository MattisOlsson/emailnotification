using System.Collections.Generic;
using System.Net.Mail;
using Geta.EmailNotification.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Geta.EmailNotification.AspNetCore
{
    public class EmailNotificationRequest : EmailNotificationRequestBase
    {
        public EmailNotificationRequest(IModelMetadataProvider modelMetadataProvider)
        {
            this.Attachments = new List<Attachment>();
            this.To = new MailAddressCollection();
            this.Cc = new MailAddressCollection();
            this.Bcc = new MailAddressCollection();
            this.ReplyTo = new MailAddressCollection();
            this.ViewData = new ViewDataDictionary(modelMetadataProvider, new ModelStateDictionary());
        }

        /// <summary>
        /// Key/value collection for placeholders
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }

        public HttpContext HttpContext { get; set; }
    }
}