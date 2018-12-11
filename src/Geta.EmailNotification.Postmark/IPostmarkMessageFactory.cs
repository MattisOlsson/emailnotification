using PostmarkDotNet;

namespace Geta.EmailNotification.Postmark
{
    public interface IPostmarkMessageFactory
    {
        PostmarkMessage Create(EmailNotificationRequest request);
    }
}