using Geta.EmailNotification.SendGrid;
using PostmarkDotNet;
using SendGrid;
using Typesafe.Mailgun;
using Geta.EmailNotification.Postmark;
using Geta.EmailNotification.MailGun;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Geta.EmailNotification.Tests.DependencyResolution
{
    public class DefaultRegistry : Registry {

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });

            For<IEmailViewRenderer>().Use(() => new EmailViewRenderer(new ViewEngineCollection { new RazorViewEngine() }));
            For<IMailMessageFactory>().Use<MailMessageFactory>();

            For<IAsyncEmailNotificationClient>().Use<SendGridEmailNotificationClient>();
            For<IAsyncEmailNotificationClient>().Use<PostmarkEmailNotificationClient>();

            For<IEmailNotificationClient>().Use<SendGridEmailNotificationClient>();
            For<IEmailNotificationClient>().Use<PostmarkEmailNotificationClient>();
            For<IEmailNotificationClient>().Use<MailGunEmailNotificationClient>();
	    
	    // TODO: update with real keys to test

            For<PostmarkClient>().Use(() => new PostmarkClient("key", "https://api.postmarkapp.com", 30));
            For<IMailgunClient>().Use(ctx => new MailgunClient("url", "key", 3));
            For<ISendGridClient>().Use(ctx => new SendGridClient("key", "https://api.sendgrid.com", null,"v3", "/mail/send"));
        }

    }
}
