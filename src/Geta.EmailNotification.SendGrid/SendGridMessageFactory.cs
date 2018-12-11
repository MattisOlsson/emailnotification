using SendGrid.Helpers.Mail;

namespace Geta.EmailNotification.SendGrid
{
    public class SendGridMessageFactory : MailMessageFactory
    {
        public SendGridMessageFactory(IEmailViewRenderer renderer) : base(renderer) { }

        public SendGridMessage CreateSendGridMessage(EmailNotificationRequest notification)
        {
            var message = base.Create(notification);
            var sendGridMessage = message.ConvertToSendGridMessage();

            return sendGridMessage;
        }
    }
}