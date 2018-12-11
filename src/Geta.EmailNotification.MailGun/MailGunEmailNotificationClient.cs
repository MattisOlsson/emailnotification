using System;
using Typesafe.Mailgun;

namespace Geta.EmailNotification.MailGun

{   /// <summary>
    /// Simple wrapper for Typesafe.Mailgun
    /// Unfortunately doesnæt suppor async sending
    /// <todo>The Typesafe.Mailgun wrapper doesn't return a status code although MailGun does. Add better error handling</todo>
    /// </summary>
    public class MailGunEmailNotificationClient : IEmailNotificationClient
    {
        private readonly IMailgunClient _mailGunClient;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MailGunEmailNotificationClient));
        private readonly IMailMessageFactory _mailMessageFactory;

        public MailGunEmailNotificationClient(
            IMailgunClient mailGunClient,
            IMailMessageFactory mailMessageFactory)
        {
            _mailGunClient = mailGunClient;
            _mailMessageFactory = mailMessageFactory;
        }

        public EmailNotificationResponse Send(EmailNotificationRequest request)
        {
            try
            {
                var mailGunRequest = _mailMessageFactory.Create(request);
                var response = _mailGunClient.SendMail(mailGunRequest);

                return new EmailNotificationResponse
                {
                    IsSent = true,
                    Message = response.Message
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