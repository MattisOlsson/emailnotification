﻿using System.Globalization;
using System.Threading.Tasks;
using Geta.EmailNotification.Shared;
using PostmarkDotNet;

namespace Geta.EmailNotification.Postmark
{
    public class PostmarkEmailNotificationClient : IEmailNotificationClient, IAsyncEmailNotificationClient
    {
        private readonly PostmarkClient _postmarkClient;
        private readonly IPostmarkMessageFactory _postmarkMessageFactory;

        public PostmarkEmailNotificationClient(
            PostmarkClient postmarkClient, IPostmarkMessageFactory postmarkMessageFactory)
        {
            _postmarkClient = postmarkClient;
            _postmarkMessageFactory = postmarkMessageFactory;
        }

        public EmailNotificationResponse Send(IEmailNotificationRequest request)
        {
            var message = _postmarkMessageFactory.Create(request);

            var response = AsyncHelper.RunSync(() => _postmarkClient.SendMessageAsync(message));

            return new EmailNotificationResponse
            {
                IsSent = response.Status == PostmarkStatus.Success,
                Message = response.ErrorCode.ToString(CultureInfo.InvariantCulture)
            };
        }

        public async Task<EmailNotificationResponse> SendAsync(IEmailNotificationRequest request)
        {
            var message = _postmarkMessageFactory.Create(request);

            var response = await _postmarkClient.SendMessageAsync(message).ConfigureAwait(false);

            return new EmailNotificationResponse
            {
                IsSent = response.Status == PostmarkStatus.Success,
                Message = response.ErrorCode.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}