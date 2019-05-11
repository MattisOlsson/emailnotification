using Geta.EmailNotification.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Geta.EmailNotification.AspNetCore
{
    public static class EmailNotificationRequestBuilderExtensions
    {
        public static IEmailNotificationRequestBuilder WithViewData(this IEmailNotificationRequestBuilder builder,
            ViewDataDictionary viewData)
        {
            return (builder as EmailNotificationRequestBuilder).WithViewData(viewData);
        }

        public static IEmailNotificationRequestBuilder WithViewData(this IEmailNotificationRequestBuilder builder, string key, object value)
        {
            return (builder as EmailNotificationRequestBuilder).WithViewData(key, value);
        }

        public static IEmailNotificationRequestBuilder WithViewModel(this IEmailNotificationRequestBuilder builder, object value)
        {
            return (builder as EmailNotificationRequestBuilder).WithViewModel(value);
        }

        public static IEmailNotificationRequestBuilder WithHttpContext(this IEmailNotificationRequestBuilder builder, HttpContext httpContext)
        {
            return (builder as EmailNotificationRequestBuilder).WithHttpContext(httpContext);
        }
    }
}