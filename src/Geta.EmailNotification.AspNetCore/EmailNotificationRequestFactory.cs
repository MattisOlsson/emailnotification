using Geta.EmailNotification.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Geta.EmailNotification.AspNetCore
{
    public class EmailNotificationRequestFactory : IEmailNotificationRequestFactory
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelMetadataProvider"></param>
        public EmailNotificationRequestFactory(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public IEmailNotificationRequest CreateEmail(IEmailNotificationRequest email = null)
        {
            return email != null
                    ? Clone(email)
                    : new EmailNotificationRequest(_modelMetadataProvider);
        }

        public IEmailNotificationRequestBuilder CreateEmailBuilder(IEmailNotificationRequest email = null)
        {
            var emailRequest = CreateEmail(email) as EmailNotificationRequest;
            return new EmailNotificationRequestBuilder(emailRequest);
        }

        private IEmailNotificationRequest Clone(IEmailNotificationRequest request)
        {
            var clone = CreateEmail() as EmailNotificationRequest;
            var emailRequest = request as EmailNotificationRequest;
            
            // Do not clone with .WithViewModel(...) as it is cloned already with .WithViewData(...)
            return new EmailNotificationRequestBuilder(clone)
                .WithAttachments(request.Attachments)
                .WithBcc(request.Bcc)
                .WithCc(request.Cc)
                .WithTo(request.To)
                .WithFrom(request.From.Address, request.From.DisplayName)
                .WithSubject(request.Subject)
                .WithViewName(request.ViewName)
                .WithViewData(emailRequest?.ViewData)
                .Build();
        }
    }
}
