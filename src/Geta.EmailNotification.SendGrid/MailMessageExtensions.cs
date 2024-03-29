﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using SendGrid.Helpers.Mail;
using Attachment = SendGrid.Helpers.Mail.Attachment;

namespace Geta.EmailNotification.SendGrid
{
    public static class MailMessageExtensions
    {
        public static EmailAddress GetSendGridAddress(this MailAddress address)
        {
            return String.IsNullOrWhiteSpace(address.DisplayName) ?
                new EmailAddress(address.Address) :
                new EmailAddress(address.Address, address.DisplayName.Replace(",", "").Replace(";", ""));
        }

        public static Attachment ConvertToSendGridAttachment(this System.Net.Mail.Attachment attachment)
        {
            using (var stream = new MemoryStream())
            {
                attachment.ContentStream.CopyTo(stream);
                return new Attachment()
                {
                    Disposition = "attachment",
                    Type = attachment.ContentType.MediaType,
                    Filename = attachment.Name,
                    ContentId = attachment.ContentId,
                    Content = Convert.ToBase64String(stream.ToArray())
                };
            }
        }

        public static SendGridMessage ConvertToSendGridMessage(this MailMessage message)
        {
            var sendgridMessage = new SendGridMessage();

            sendgridMessage.From = GetSendGridAddress(message.From);

            if (message.ReplyToList.Any())
            {
                sendgridMessage.ReplyTo = message.ReplyToList.First().GetSendGridAddress();
            }

            if (message.To.Any())
            {
                var tos = message.To.Select(x => x.GetSendGridAddress()).ToList();
                sendgridMessage.AddTos(tos);
            }

            if (message.CC.Any())
            {
                var cc = message.CC.Select(x => x.GetSendGridAddress()).ToList();
                sendgridMessage.AddCcs(cc);
            }

            if (message.Bcc.Any())
            {
                var bcc = message.Bcc.Select(x => x.GetSendGridAddress()).ToList();
                sendgridMessage.AddBccs(bcc);
            }

            if (!string.IsNullOrWhiteSpace(message.Subject))
            {
                sendgridMessage.Subject = message.Subject;
            }

            if (!string.IsNullOrWhiteSpace(message.Body))
            {
                var content = message.Body;

                if (message.IsBodyHtml)
                {
                    content = content.Replace("\r", string.Empty).Replace("\n", string.Empty);
                    if (content.StartsWith("<html"))
                    {
                        content = message.Body;
                    }
                    else
                    {
                        content = $"<html><body>{message.Body}</body></html>";
                    }

                    sendgridMessage.AddContent("text/html", content);
                }
                else
                {
                    sendgridMessage.AddContent("text/plain", content);
                }
            }

            if (message.Attachments.Any())
            {
                sendgridMessage.Attachments = new List<Attachment>();
                sendgridMessage.Attachments.AddRange(message.Attachments.Select(ConvertToSendGridAttachment));
            }

            return sendgridMessage;
        }
    }
}