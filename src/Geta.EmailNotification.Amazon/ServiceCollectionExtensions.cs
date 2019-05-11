#if NETSTANDARD
using Geta.EmailNotification.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Geta.EmailNotification.Amazon
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAmazonEmailNotificationClient(this IServiceCollection services)
        {
            services
                .RemoveAll<IEmailNotificationClient>()
                .RemoveAll<IAsyncEmailNotificationClient>();

            services
                .AddScoped<IEmailNotificationClient, AmazonEmailNotificationClient>()
                .AddScoped<IAsyncEmailNotificationClient, AmazonEmailNotificationClient>();
        }
    }
}
#endif