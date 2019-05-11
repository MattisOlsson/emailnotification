﻿using System.Web.Mvc;
using Geta.EmailNotification.AspNetCore;
using Geta.EmailNotification.Shared;

namespace Geta.EmailNotification
{
    public class EmailNotificationRequestBuilder : EmailNotificationRequestBuilderBase
    {
        private readonly EmailNotificationRequest _request;

        /// <summary>
        /// Creates new instance of EmailNotificationRequestBuilder.
        /// </summary>
        /// <param name="request">Existing EmailNotificationRequest from which to copy values. Creates empty EmailNotificationRequest if null passed.</param>
        public EmailNotificationRequestBuilder(IEmailNotificationRequest request = null) : base(request)
        {
            var emailRequest = request != null ? Clone(request) as EmailNotificationRequest : new EmailNotificationRequest();
            _request = emailRequest;
        }

        /// <summary>
        /// Adds ViewData values to existing ViewData dictionary. ViewData values can be used in Razor view to render email.
        /// </summary>
        /// <param name="key">Key of the value.</param>
        /// <param name="value">Value.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public EmailNotificationRequestBuilder WithViewData(string key, object value)
        {
            _request.ViewData.Add(key, value);
            return this;
        }

        /// <summary>
        /// Adds multiple ViewData values from ViewDataDictionary to existing ViewData dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary of ViewData values.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public EmailNotificationRequestBuilder WithViewData(ViewDataDictionary dictionary)
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
        public EmailNotificationRequestBuilder WithViewModel(object value)
        {
            _request.ViewData.Model = value;
            return this;
        }

        private static IEmailNotificationRequest Clone(IEmailNotificationRequest request)
        {
            var emailRequest = request as EmailNotificationRequest;

            // Do not clone with .WithViewModel(...) as it is cloned already with .WithViewData(...)
            return new EmailNotificationRequestBuilder()
                .WithAttachments(request.Attachments)
                .WithBcc(request.Bcc)
                .WithCc(request.Cc)
                .WithTo(request.To)
                .WithFrom(request.From.Address, request.From.DisplayName)
                .WithSubject(request.Subject)
                .WithViewName(request.ViewName)
                .WithViewData(emailRequest?.ViewData)
                .Build();
        }
    }
}