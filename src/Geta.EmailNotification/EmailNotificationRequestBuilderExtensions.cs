using System.Web.Mvc;
using Geta.EmailNotification.Shared;

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
    }
}