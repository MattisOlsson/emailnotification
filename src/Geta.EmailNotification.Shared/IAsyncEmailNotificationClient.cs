using System.Threading.Tasks;

namespace Geta.EmailNotification.Shared
{
    public interface IAsyncEmailNotificationClient
    {
        Task<EmailNotificationResponse> SendAsync(IEmailNotificationRequest request);
    }
}