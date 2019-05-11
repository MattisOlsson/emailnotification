using Geta.EmailNotification.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geta.EmailNotification.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEmailNotification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMailMessageFactory, MailMessageFactory>();
            services.AddScoped<IEmailNotificationRequestFactory, EmailNotificationRequestFactory>();
            services.AddScoped<IEmailViewRenderer, EmailViewRenderer>();
            UrlHelperExtensions.Configure(configuration);
        }
    }
}