using System.Linq;
using System.Net.Mail;
using PostmarkDotNet;

namespace Geta.EmailNotification.Postmark.Extensions
{
    public static class PostmarkExtensions
    {
        public static PostmarkMessage ToPostmarkMessage(this MailMessage mailMessage)
        {
            var postmarkMessage = new PostmarkMessage
            {
                From = mailMessage.From.Address,
                ReplyTo = mailMessage.ReplyToList.FirstOrDefault()?.Address,
                To = string.Join(",", mailMessage.To.Select(to => to.Address)),
                Cc = string.Join(",", mailMessage.CC.Select(cc => cc.Address)),
                Bcc = string.Join(",", mailMessage.Bcc.Select(bcc => bcc.Address)),
                Subject = mailMessage.Subject,
                HtmlBody = mailMessage.IsBodyHtml 
                    ? mailMessage.Body
                    : null,
                TextBody = !mailMessage.IsBodyHtml
                    ? mailMessage.Body
                    : null
            };

            foreach (var mailAttachment in mailMessage.Attachments)
            {
                postmarkMessage.AddAttachment(mailAttachment.ContentStream, mailAttachment.Name, mailAttachment.ContentType.MediaType, mailAttachment.ContentId);
            }

            return postmarkMessage;
        }
    }
}