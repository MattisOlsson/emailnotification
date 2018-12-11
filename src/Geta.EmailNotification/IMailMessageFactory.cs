using System.Net.Mail;

namespace Geta.EmailNotification
{
    public interface IMailMessageFactory
    {
        MailMessage Create(EmailNotificationRequest request);
    }
}