using Geta.EmailNotification.Postmark.Extensions;
using Geta.EmailNotification.Shared;
using PostmarkDotNet;

namespace Geta.EmailNotification.Postmark
{
    public class PostmarkMessageFactory : IPostmarkMessageFactory
    {
        private readonly IMailMessageFactory _mailMessageFactory;

        public PostmarkMessageFactory(IMailMessageFactory mailMessageFactory)
        {
            _mailMessageFactory = mailMessageFactory;
        }

        public PostmarkMessage Create(IEmailNotificationRequest request)
        {
            return _mailMessageFactory.Create(request).ToPostmarkMessage();
        }
    }
}