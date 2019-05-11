using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Geta.EmailNotification.AspNetCore
{
    /// <summary>
    /// Extension methods for IUrlHelper
    /// </summary>
    public static class UrlHelperExtensions
    {
        private static IConfiguration _configuration;

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This extension method will help generating Absolute Urls in the mailer or other views
        /// </summary>
        /// <param name="urlHelper">The object that gets the extended behavior</param>
        /// <param name="relativeOrAbsoluteUrl">A relative or absolute URL to convert to Absolute</param>
        /// <returns>An absolute Url. e.g. https://domain:port/controller/action from /controller/action</returns>
        public static string Absolute(this IUrlHelper urlHelper, string relativeOrAbsoluteUrl)
        {
            var uri = new Uri(relativeOrAbsoluteUrl, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                return relativeOrAbsoluteUrl;
            }

            if (Uri.TryCreate(BaseUrl(urlHelper), urlHelper.Content(relativeOrAbsoluteUrl), out var combinedUri))
            {
                return combinedUri.AbsoluteUri;
            }

            throw new Exception($"Could not create absolute url for {relativeOrAbsoluteUrl} using baseUri: {BaseUrl(urlHelper)}");
        }


        private static Uri BaseUrl(IUrlHelper urlHelper)
        {
            var section = _configuration.GetSection(nameof(EmailNotificationSettings));
            var settings = section.Get<EmailNotificationSettings>();
            var baseUrl = settings?.BaseUrl;

            //No configuration given, so use the one from the context
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                var request = urlHelper.ActionContext.HttpContext.Request;
                baseUrl = string.Concat(
                    request.Scheme,
                    "://",
                    request.Host.ToUriComponent()
                );
            }

            return new Uri(baseUrl);
        }
    }
}