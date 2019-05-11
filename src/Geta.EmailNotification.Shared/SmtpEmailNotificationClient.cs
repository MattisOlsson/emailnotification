using System;
using System.Net.Mail;
using System.Threading.Tasks;
using log4net;

namespace Geta.EmailNotification.Shared
{
    public class SmtpEmailNotificationClient : IEmailNotificationClient, IAsyncEmailNotificationClient
    {
        private readonly IMailMessageFactory _mailMessageFactory;
        private static readonly ILog Log = LogManager.GetLogger(typeof(SmtpEmailNotificationClient));

        public SmtpEmailNotificationClient(IMailMessageFactory mailMessageFactory)
        {
            _mailMessageFactory = mailMessageFactory;
        }

        public EmailNotificationResponse Send(IEmailNotificationRequest request)
        {
            var response = new EmailNotificationResponse();

            try
            {
                using (var mail = _mailMessageFactory.Create(request))
                using (var client = new SmtpClient())
                {
                    client.Send(mail);
                }

                response.IsSent = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                Log.Error($"Email failed to: {request.To}. Subject: {request.Subject}", e);
            }

            return response;
        }

        public async Task<EmailNotificationResponse> SendAsync(IEmailNotificationRequest request)
        {
            var response = new EmailNotificationResponse();

            try
            {
                using (var mail = _mailMessageFactory.Create(request))
                using (var client = new SmtpClient())
                {
                    await client.SendMailAsync(mail).ConfigureAwait(false);
                }

                response.IsSent = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                Log.Error($"Email failed to: {request.To}. Subject: {request.Subject}", e);
            }

            return response;
        }
    }
}