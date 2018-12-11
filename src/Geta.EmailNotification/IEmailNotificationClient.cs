namespace Geta.EmailNotification
{
    public interface IEmailNotificationClient
    {
        EmailNotificationResponse Send(EmailNotificationRequest request);
    }
}