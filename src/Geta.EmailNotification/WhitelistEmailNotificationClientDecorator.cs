using System;
using System.Linq;
using System.Net.Mail;

namespace Geta.EmailNotification
{
    public class WhitelistEmailNotificationClientDecorator : IEmailNotificationClient
    {
        private readonly IEmailNotificationClient _emailClient;
        private readonly WhitelistConfiguration _whitelistConfiguration;

        public WhitelistEmailNotificationClientDecorator(
            IEmailNotificationClient emailClient,
            WhitelistConfiguration whitelistConfiguration)
        {
            _emailClient = emailClient;
            _whitelistConfiguration = whitelistConfiguration;
        }

        public EmailNotificationResponse Send(EmailNotificationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "EmailNotificationRequest cannot be null");
            }

            if (!_whitelistConfiguration.HasWhitelist)
            {
                return _emailClient.Send(request);
            }

            request.To = WhiteList(request.To);
            request.Cc = WhiteList(request.Cc);
            request.Bcc = WhiteList(request.Bcc);

            return _emailClient.Send(request);
        }

        private MailAddressCollection WhiteList(MailAddressCollection addressCollection)
        {
            var result = new MailAddressCollection();
            foreach (var address in addressCollection)
            {
                if (InWhitelist(address.Address))
                {
                    result.Add(address);
                }
            }
            return result;
        }

        private bool InWhitelist(string address)
        {
            return _whitelistConfiguration.Emails.Any(address.Equals)
                || _whitelistConfiguration.Domains.Any(address.EndsWith);
        }
    }
}