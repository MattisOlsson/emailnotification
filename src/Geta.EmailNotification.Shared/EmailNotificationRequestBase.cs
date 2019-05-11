using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Geta.EmailNotification.Shared
{
    public abstract class EmailNotificationRequestBase : IEmailNotificationRequest
    {
        /// <summary>
        /// From email address
        /// </summary>
        public MailAddress From { get; set; }

        /// <summary>
        /// To email address'
        /// </summary>
        public MailAddressCollection To { get; set; }

        /// <summary>
        /// Copy email address'
        /// </summary>
        public MailAddressCollection Cc { get; set; }

        /// <summary>
        /// Blind copy email address'
        /// </summary>
        public MailAddressCollection Bcc { get; set; }

        /// <summary>
        /// Reply to email address'
        /// </summary>
        public MailAddressCollection ReplyTo { get; set; }

        public string Subject { get; set; }

        /// <summary>
        /// HTML content for HTML emails
        /// </summary>
        public string HtmlBody { get; set; }

        /// <summary>
        /// Text content for fallback or text only emails
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Razor view name (without .cshtml)
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// By default we try and send asynchronous
        /// </summary>
        [Obsolete("This property is not in use anymore. IEmailNotificationClient.Send will be always synchronous. For async use IAsyncEmailNotificationClient.SendAsync.")]
        public bool SendSynchronous { get; set; }

        /// <summary>
        /// Attachments for this email message
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}