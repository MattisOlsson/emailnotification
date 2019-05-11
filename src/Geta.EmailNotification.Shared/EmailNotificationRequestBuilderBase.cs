using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Geta.EmailNotification.Shared
{
    public abstract class EmailNotificationRequestBuilderBase : IEmailNotificationRequestBuilder
    {
        private readonly IEmailNotificationRequest _request;

        protected EmailNotificationRequestBuilderBase(IEmailNotificationRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// Sets Subject.
        /// </summary>
        /// <param name="subject">Email subject.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithSubject(string subject)
        {
            _request.Subject = subject;
            return this;
        }

        /// <summary>
        /// Sets From email address.
        /// </summary>
        /// <param name="email">From email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithFrom(string email)
        {
            _request.From = new MailAddress(email);
            return this;
        }

        /// <summary>
        /// Sets From email address with display name.
        /// </summary>
        /// <param name="email">From email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithFrom(string email, string displayName)
        {
            _request.From = new MailAddress(email, displayName);
            return this;
        }

        /// <summary>
        /// Adds To email address to current To email addresses.
        /// </summary>
        /// <param name="email">To email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithTo(string email)
        {
            _request.To.Add(new MailAddress(email));
            return this;
        }

        /// <summary>
        /// Adds To email address with display name to current To email addresses.
        /// </summary>
        /// <param name="email">To email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithTo(string email, string displayName)
        {
            _request.To.Add(new MailAddress(email, displayName));
            return this;
        }

        /// <summary>
        /// Adds multiple To email addresses from MailAddressCollection to current To email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with To addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithTo(MailAddressCollection toEmails)
        {
            CopyEmails(toEmails, _request.To);
            return this;
        }

        /// <summary>
        /// Adds multiple To email addresses from sequence of Tuple email address and display name to current To email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is To email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithTo(IEnumerable<Tuple<string, string>> toEmails)
        {
            var list = toEmails.ToList();
            list.ForEach(x => _request.To.Add(new MailAddress(x.Item1, x.Item2)));
            return this;
        }

        /// <summary>
        /// Adds Cc email address to current Cc email addresses.
        /// </summary>
        /// <param name="email">Cc email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithCc(string email)
        {
            _request.Cc.Add(new MailAddress(email));
            return this;
        }

        /// <summary>
        /// Adds Cc email address with display name to current Cc email addresses.
        /// </summary>
        /// <param name="email">Cc email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithCc(string email, string displayName)
        {
            _request.Cc.Add(new MailAddress(email, displayName));
            return this;
        }

        /// <summary>
        /// Adds multiple Cc email addresses from MailAddressCollection to current Cc email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with Cc addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithCc(MailAddressCollection toEmails)
        {
            CopyEmails(toEmails, _request.Cc);
            return this;
        }

        /// <summary>
        /// Adds multiple Cc email addresses from sequence of Tuple email address and display name to current Cc email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is Cc email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithCc(IEnumerable<Tuple<string, string>> toEmails)
        {
            var list = toEmails.ToList();
            list.ForEach(x => _request.Cc.Add(new MailAddress(x.Item1, x.Item2)));
            return this;
        }

        /// <summary>
        /// Adds Bcc email address to current Bcc email addresses.
        /// </summary>
        /// <param name="email">Bcc email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithBcc(string email)
        {
            _request.Bcc.Add(new MailAddress(email));
            return this;
        }

        /// <summary>
        /// Adds Bcc email address with display name to current Bcc email addresses.
        /// </summary>
        /// <param name="email">Bcc email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithBcc(string email, string displayName)
        {
            _request.Bcc.Add(new MailAddress(email, displayName));
            return this;
        }

        /// <summary>
        /// Adds multiple Bcc email addresses from MailAddressCollection to current Bcc email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with Bcc addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithBcc(MailAddressCollection toEmails)
        {
            CopyEmails(toEmails, _request.Bcc);
            return this;
        }

        /// <summary>
        /// Adds multiple Bcc email addresses from sequence of Tuple email address and display name to current Bcc email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is Bcc email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithBcc(IEnumerable<Tuple<string, string>> toEmails)
        {
            var list = toEmails.ToList();
            list.ForEach(x => _request.Bcc.Add(new MailAddress(x.Item1, x.Item2)));
            return this;
        }

        /// <summary>
        /// Adds ReplyTo email address to current ReplyTo email addresses.
        /// </summary>
        /// <param name="email">ReplyTo email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithReplyTo(string email)
        {
            _request.ReplyTo.Add(new MailAddress(email));
            return this;
        }

        /// <summary>
        /// Adds ReplyTo email address with display name to current ReplyTo email addresses.
        /// </summary>
        /// <param name="email">ReplyTo email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithReplyTo(string email, string displayName)
        {
            _request.ReplyTo.Add(new MailAddress(email, displayName));
            return this;
        }

        /// <summary>
        /// Adds multiple ReplyTo email addresses from MailAddressCollection to current ReplyTo email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with ReplyTo addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithReplyTo(MailAddressCollection toEmails)
        {
            CopyEmails(toEmails, _request.ReplyTo);
            return this;
        }

        /// <summary>
        /// Adds multiple ReplyTo email addresses from sequence of Tuple email address and display name to current ReplyTo email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is ReplyTo email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithReplyTo(IEnumerable<Tuple<string, string>> toEmails)
        {
            var list = toEmails.ToList();
            list.ForEach(x => _request.ReplyTo.Add(new MailAddress(x.Item1, x.Item2)));
            return this;
        }

        /// <summary>
        /// Sets Razor view's ViewName to render email of.
        /// </summary>
        /// <param name="viewName">Razor view's ViewName.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithViewName(string viewName)
        {
            _request.ViewName = viewName;
            return this;
        }

        /// <summary>
        /// Adds Attachment to existing Attachment collection.
        /// </summary>
        /// <param name="attachment">Attachment.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithAttachment(Attachment attachment)
        {
            _request.Attachments.Add(attachment);
            return this;
        }

        /// <summary>
        /// Adds multiple Attachment values to existing Attachment collection.
        /// </summary>
        /// <param name="attachments">Sequence of multiple Attachment values.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        public virtual IEmailNotificationRequestBuilder WithAttachments(IEnumerable<Attachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                _request.Attachments.Add(attachment);
            }

            return this;
        }

        /// <summary>
        /// Creates EmailNotificationRequest instance.
        /// </summary>
        /// <returns>Instance of EmailNotificationRequest.</returns>
        public virtual IEmailNotificationRequest Build()
        {
            return _request;
        }

        protected virtual void CopyEmails(IEnumerable<MailAddress> input, MailAddressCollection output)
        {
            foreach (var inp in input)
            {
                output.Add(inp);
            }
        }

    }
}