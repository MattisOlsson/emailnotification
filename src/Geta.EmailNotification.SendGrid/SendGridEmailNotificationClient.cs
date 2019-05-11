﻿using SendGrid;
using System;
using System.Net;
using System.Threading.Tasks;
using Geta.EmailNotification.Shared;

namespace Geta.EmailNotification.SendGrid
{
    public class SendGridEmailNotificationClient : IEmailNotificationClient, IAsyncEmailNotificationClient
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly SendGridMessageFactory _mailMessageFactory;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SendGridEmailNotificationClient));

        public SendGridEmailNotificationClient(ISendGridClient sendGridClient, SendGridMessageFactory mailMessageFactory)
        {
            _sendGridClient = sendGridClient;
            _mailMessageFactory = mailMessageFactory;
        }

        public async Task<EmailNotificationResponse> SendAsync(IEmailNotificationRequest request)
        {
            try
            {
                var message = _mailMessageFactory.CreateSendGridMessage(request);
                var response = await _sendGridClient.SendEmailAsync(message);

                return new EmailNotificationResponse
                {
                    IsSent = response.StatusCode == HttpStatusCode.Accepted,
                    Message = await response.Body.ReadAsStringAsync()
                };
            }
            catch (Exception ex)
            {
                Log.Error($"Email failed to: {request.To}. Subject: {request.Subject}.", ex);

                return new EmailNotificationResponse
                {
                    Message = ex.Message
                };
            }
        }

        public EmailNotificationResponse Send(IEmailNotificationRequest request)
        {
            try
            {
                var message = _mailMessageFactory.CreateSendGridMessage(request);

                var response = AsyncHelper.RunSync(() => _sendGridClient.SendEmailAsync(message));

                return new EmailNotificationResponse
                {
                    IsSent = response.StatusCode == HttpStatusCode.Accepted,
                    Message = response.Body.ReadAsStringAsync().Result
                };
            }
            catch (Exception ex)
            {
                Log.Error($"Email failed to: {request.To}. Subject: {request.Subject}.", ex);

                return new EmailNotificationResponse
                {
                    Message = ex.Message
                };
            }
        }
    }
}