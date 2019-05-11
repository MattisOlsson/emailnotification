using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Geta.EmailNotification.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Geta.EmailNotification.AspNetCore
{
    /// <summary>
    /// Renders <see cref="Email"/> view's into raw strings using the MVC ViewEngine infrastructure.
    /// Source: https://github.com/andrewdavey/postal
    /// </summary>
    public class EmailViewRenderer : IEmailViewRenderer
    {
        /// <summary>
        /// Creates a new <see cref="IEmailViewRenderer"/> that uses the given view engines.
        /// </summary>
        /// <param name="viewEngines">The view engines to use when rendering email views.</param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="tempDataProvider"></param>
        /// <param name="serviceProvider"></param>
        public EmailViewRenderer(ICompositeViewEngine viewEngines, IHttpContextAccessor httpContextAccessor, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _viewEngines = viewEngines;
            _httpContextAccessor = httpContextAccessor;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            EmailViewDirectoryName = "Emails";
        }

        private readonly ICompositeViewEngine _viewEngines;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// The name of the directory in "Views" that contains the email views.
        /// By default, this is "Emails".
        /// </summary>
        public string EmailViewDirectoryName { get; set; }

        /// <summary>
        /// Renders an email view.
        /// </summary>
        /// <param name="email">The email to render.</param>
        /// <param name="viewName">Optional email view name override. If null then the email's ViewName property is used instead.</param>
        /// <returns>The rendered email view output.</returns>
        public string Render(IEmailNotificationRequest email, string viewName = null)
        {
            var typedEmail = GetTypedEmailOrThrow(email);
            viewName = viewName ?? email.ViewName;
            var actionContext = CreateActionContext(typedEmail);
            var view = CreateView(viewName, actionContext);
            var viewOutput = Task.Run(() => RenderView(view, typedEmail.ViewData, actionContext)).Result;
            return viewOutput;
        }

        public async Task<string> RenderAsync(IEmailNotificationRequest email, string viewName = null)
        {
            var typedEmail = GetTypedEmailOrThrow(email);
            viewName = viewName ?? email.ViewName;
            var actionContext = CreateActionContext(typedEmail);
            var view = CreateView(viewName, actionContext);
            var viewOutput = await RenderView(view, typedEmail.ViewData, actionContext);
            return viewOutput;
        }

        EmailNotificationRequest GetTypedEmailOrThrow(IEmailNotificationRequest email)
        {
            if (email is EmailNotificationRequest typedEmail)
            {
                return typedEmail;
            }

            throw new Exception("Invalid type for email. EmailNotificationRequest was expected.");
        }

        HttpContext GetHttpContext(EmailNotificationRequest email)
        {
            return email.HttpContext ?? _httpContextAccessor.HttpContext ?? new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };
        }

        ActionContext CreateActionContext(EmailNotificationRequest email)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = EmailViewDirectoryName;

            var actionContext = new ActionContext
            {
                HttpContext = GetHttpContext(email),
                RouteData = routeData,
                ActionDescriptor = new ControllerActionDescriptor
                {
                    RouteValues = new Dictionary<string, string>
                    {
                        { "controller", EmailViewDirectoryName }
                    },
                    ControllerName = "Emails"
                }
            };

            return actionContext;
        }

        IView CreateView(string viewName, ActionContext actionContext)
        {
            var result = _viewEngines.FindView(actionContext, viewName, false);
            if (result.View != null)
                return result.View;

            throw new Exception(
                "Email view not found for " + viewName +
                ". Locations searched:" + Environment.NewLine +
                string.Join(Environment.NewLine, result.SearchedLocations)
            );
        }

        async Task<string> RenderView(IView view, ViewDataDictionary viewData, ActionContext actionContext)
        {
            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewData,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    writer,
                    new HtmlHelperOptions()
                );

                await view.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}