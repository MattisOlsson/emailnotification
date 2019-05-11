using Geta.EmailNotification.Shared;
using SendGrid.Helpers.Mail;

namespace Geta.EmailNotification.SendGrid
{
    public class SendGridMessageFactory : MailMessageFactory
    {
        public SendGridMessageFactory(IEmailViewRenderer renderer) : base(renderer) { }

        public SendGridMessage CreateSendGridMessage(IEmailNotificationRequest notification)
        {
            var message = base.Create(notification);
            var sendGridMessage = message.ConvertToSendGridMessage();

            return sendGridMessage;
        }
    }
}