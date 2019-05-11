using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Geta.EmailNotification.Shared
{
    public class MailMessageFactory : IMailMessageFactory
    {
        private readonly IEmailViewRenderer _renderer;

        public MailMessageFactory(IEmailViewRenderer renderer)
        {
            _renderer = renderer;
        }

        public MailMessage Create(IEmailNotificationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "EmailNotificationRequest cannot be null");
            }

            if (request.To == null)
            {
                throw new ArgumentNullException(
                    $"{nameof(request)}.{nameof(request.To)}", "To email address cannot be null");
            }

            if (request.From == null)
            {
                throw new ArgumentNullException(
                    $"{nameof(request)}.{nameof(request.From)}", "From email address cannot be null");
            }

            var mail = new MailMessage
            {
                Subject = request.Subject,
                From = request.From
            };
            CopyAddress(request.To, mail.To);
            CopyAddress(request.Cc, mail.CC);
            CopyAddress(request.Bcc, mail.Bcc);
            CopyAddress(request.ReplyTo, mail.ReplyToList);
            CopyAttachments(request.Attachments, mail.Attachments);

            bool isHtml;
            mail.Body = CreateBody(request, out isHtml);
            mail.IsBodyHtml = isHtml;

            mail.BodyEncoding = Encoding.UTF8;

            return mail;
        }

        private static void CopyAddress(IEnumerable<MailAddress> from, MailAddressCollection to)
        {
            foreach (var mailAddress in from)
            {
                to.Add(mailAddress);
            }
        }

        private static void CopyAttachments(IEnumerable<Attachment> from, AttachmentCollection to)
        {
            foreach (var attachment in from)
            {
                to.Add(attachment);
            }
        }

        private string CreateBody(IEmailNotificationRequest request, out bool isHtml)
        {
            isHtml = true;
            if (!string.IsNullOrWhiteSpace(request.ViewName))
            {
                return _renderer.Render(request);
            }

            if (!string.IsNullOrEmpty(request.HtmlBody))
            {
                return request.HtmlBody;
            }

            isHtml = false;
            return request.Body;
        }
    }
}