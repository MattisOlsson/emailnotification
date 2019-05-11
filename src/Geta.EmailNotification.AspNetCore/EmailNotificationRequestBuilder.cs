using Geta.EmailNotification.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Geta.EmailNotification.AspNetCore
{
    public class EmailNotificationRequestBuilder : EmailNotificationRequestBuilderBase
    {
        private readonly EmailNotificationRequest _request;

        /// <summary>
        /// Creates new instance of EmailNotificationRequestBuilder.
        /// </summary>
        /// <param name="request">Existing EmailNotificationRequest from which to copy values. Creates empty EmailNotificationRequest if null passed.</param>
        public EmailNotificationRequestBuilder(EmailNotificationRequest request) : base(request)
        {
            _request = request;
        }

        /// <summary>
        /// Adds ViewData values to existing ViewData dictionary. ViewData values can be used in Razor view to render email.
        /// </summary>
        /// <param name="key">Key of the value.</param>
        /// <param name="value">Value.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public IEmailNotificationRequestBuilder WithViewData(string key, object value)
        {
            _request.ViewData.Add(key, value);
            return this;
        }

        /// <summary>
        /// Adds multiple ViewData values from ViewDataDictionary to existing ViewData dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary of ViewData values.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public IEmailNotificationRequestBuilder WithViewData(ViewDataDictionary dictionary)
        {
            foreach (var pair in dictionary)
            {
                _request.ViewData.Add(pair);
            }

            return this;
        }

        /// <summary>
        /// Sets Model value on ViewData. Model can be used in Razor view to render email.
        /// </summary>
        /// <param name="value">View model's value.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public IEmailNotificationRequestBuilder WithViewModel(object value)
        {
            _request.ViewData.Model = value;
            return this;
        }

        public IEmailNotificationRequestBuilder WithHttpContext(HttpContext httpContext)
        {
            _request.HttpContext = httpContext;
            return this;
        }
    }
}