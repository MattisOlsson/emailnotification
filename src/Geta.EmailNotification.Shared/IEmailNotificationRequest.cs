using System.Collections.Generic;
using System.Net.Mail;

namespace Geta.EmailNotification.Shared
{
    public interface IEmailNotificationRequest
    {
        /// <summary>
        /// From email address
        /// </summary>
        MailAddress From { get; set; }

        /// <summary>
        /// To email address'
        /// </summary>
        MailAddressCollection To { get; set; }

        /// <summary>
        /// Copy email address'
        /// </summary>
        MailAddressCollection Cc { get; set; }

        /// <summary>
        /// Blind copy email address'
        /// </summary>
        MailAddressCollection Bcc { get; set; }

        /// <summary>
        /// Reply to email address'
        /// </summary>
        MailAddressCollection ReplyTo { get; set; }

        string Subject { get; set; }

        /// <summary>
        /// Text content for fallback or text only emails
        /// </summary>
        string Body { get; set; }

        string HtmlBody { get; set; }

        /// <summary>
        /// Razor View name (.cshtml)
        /// </summary>
        string ViewName { get; set; }

        /// <summary>
        /// Attachments for this email message
        /// </summary>
        List<Attachment> Attachments { get; set; }
    }
}