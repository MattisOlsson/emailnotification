using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Geta.EmailNotification
{
    public class SmtpEmailNotificationClient : IEmailNotificationClient, IAsyncEmailNotificationClient
    {
        private readonly IMailMessageFactory _mailMessageFactory;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SmtpEmailNotificationClient));

        public SmtpEmailNotificationClient(IMailMessageFactory mailMessageFactory)
        {
            _mailMessageFactory = mailMessageFactory;
        }

        public EmailNotificationResponse Send(EmailNotificationRequest request)
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

        public async Task<EmailNotificationResponse> SendAsync(EmailNotificationRequest request)
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