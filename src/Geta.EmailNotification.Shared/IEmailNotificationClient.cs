namespace Geta.EmailNotification.Shared
{
    public interface IEmailNotificationClient
    {
        EmailNotificationResponse Send(IEmailNotificationRequest request);
    }
}