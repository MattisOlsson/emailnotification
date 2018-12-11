using System.Threading.Tasks;

namespace Geta.EmailNotification
{
    public interface IAsyncEmailNotificationClient
    {
        Task<EmailNotificationResponse> SendAsync(EmailNotificationRequest request);
    }
}