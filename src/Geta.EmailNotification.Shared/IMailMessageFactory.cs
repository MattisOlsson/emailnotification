using System.Net.Mail;

namespace Geta.EmailNotification.Shared
{
    public interface IMailMessageFactory
    {
        MailMessage Create(IEmailNotificationRequest request);
    }
}