using Geta.EmailNotification.Shared;
using PostmarkDotNet;

namespace Geta.EmailNotification.Postmark
{
    public interface IPostmarkMessageFactory
    {
        PostmarkMessage Create(IEmailNotificationRequest request);
    }
}