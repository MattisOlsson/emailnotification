using Geta.EmailNotification.Shared;

namespace Geta.EmailNotification.AspNetCore
{
    public interface IEmailNotificationRequestFactory
    {
        IEmailNotificationRequest CreateEmail(IEmailNotificationRequest email = null);
        IEmailNotificationRequestBuilder CreateEmailBuilder(IEmailNotificationRequest email = null);
    }
}