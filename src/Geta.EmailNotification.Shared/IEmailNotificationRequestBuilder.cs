using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Geta.EmailNotification.Shared
{
    public interface IEmailNotificationRequestBuilder
    {
        IEmailNotificationRequestBuilder WithSubject(string subject);

        /// <summary>
        /// Sets From email address.
        /// </summary>
        /// <param name="email">From email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithFrom(string email);

        /// <summary>
        /// Sets From email address with display name.
        /// </summary>
        /// <param name="email">From email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithFrom(string email, string displayName);

        /// <summary>
        /// Adds To email address to current To email addresses.
        /// </summary>
        /// <param name="email">To email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithTo(string email);

        /// <summary>
        /// Adds To email address with display name to current To email addresses.
        /// </summary>
        /// <param name="email">To email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithTo(string email, string displayName);

        /// <summary>
        /// Adds multiple To email addresses from MailAddressCollection to current To email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with To addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithTo(MailAddressCollection toEmails);

        /// <summary>
        /// Adds multiple To email addresses from sequence of Tuple email address and display name to current To email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is To email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithTo(IEnumerable<Tuple<string, string>> toEmails);

        /// <summary>
        /// Adds Cc email address to current Cc email addresses.
        /// </summary>
        /// <param name="email">Cc email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithCc(string email);

        /// <summary>
        /// Adds Cc email address with display name to current Cc email addresses.
        /// </summary>
        /// <param name="email">Cc email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithCc(string email, string displayName);

        /// <summary>
        /// Adds multiple Cc email addresses from MailAddressCollection to current Cc email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with Cc addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithCc(MailAddressCollection toEmails);

        /// <summary>
        /// Adds multiple Cc email addresses from sequence of Tuple email address and display name to current Cc email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is Cc email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithCc(IEnumerable<Tuple<string, string>> toEmails);

        /// <summary>
        /// Adds Bcc email address to current Bcc email addresses.
        /// </summary>
        /// <param name="email">Bcc email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithBcc(string email);

        /// <summary>
        /// Adds Bcc email address with display name to current Bcc email addresses.
        /// </summary>
        /// <param name="email">Bcc email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithBcc(string email, string displayName);

        /// <summary>
        /// Adds multiple Bcc email addresses from MailAddressCollection to current Bcc email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with Bcc addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithBcc(MailAddressCollection toEmails);

        /// <summary>
        /// Adds multiple Bcc email addresses from sequence of Tuple email address and display name to current Bcc email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is Bcc email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithBcc(IEnumerable<Tuple<string, string>> toEmails);

        /// <summary>
        /// Adds ReplyTo email address to current ReplyTo email addresses.
        /// </summary>
        /// <param name="email">ReplyTo email address.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithReplyTo(string email);

        /// <summary>
        /// Adds ReplyTo email address with display name to current ReplyTo email addresses.
        /// </summary>
        /// <param name="email">ReplyTo email address.</param>
        /// <param name="displayName">Display name.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithReplyTo(string email, string displayName);

        /// <summary>
        /// Adds multiple ReplyTo email addresses from MailAddressCollection to current ReplyTo email addresses.
        /// </summary>
        /// <param name="toEmails">MailAddressCollection with ReplyTo addresses.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithReplyTo(MailAddressCollection toEmails);

        /// <summary>
        /// Adds multiple ReplyTo email addresses from sequence of Tuple email address and display name to current ReplyTo email addresses. 
        /// </summary>
        /// <param name="toEmails">Sequence of Tuple where Item1 is ReplyTo email and Item2 is display name in the Tuple.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithReplyTo(IEnumerable<Tuple<string, string>> toEmails);

        /// <summary>
        /// Sets Razor view's ViewName to render email of.
        /// </summary>
        /// <param name="viewName">Razor view's ViewName.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithViewName(string viewName);

        /// <summary>
        /// Adds Attachment to existing Attachment collection.
        /// </summary>
        /// <param name="attachment">Attachment.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithAttachment(Attachment attachment);

        /// <summary>
        /// Adds multiple Attachment values to existing Attachment collection.
        /// </summary>
        /// <param name="attachments">Sequence of multiple Attachment values.</param>
        /// <returns>Current EmailNotificationRequestBuilder instance.</returns>
        IEmailNotificationRequestBuilder WithAttachments(IEnumerable<Attachment> attachments);

        /// <summary>
        /// Creates EmailNotificationRequest instance.
        /// </summary>
        /// <returns>Instance of EmailNotificationRequest.</returns>
        IEmailNotificationRequest Build();
    }
}