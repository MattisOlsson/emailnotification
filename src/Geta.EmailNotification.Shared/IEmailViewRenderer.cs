using System.Threading.Tasks;

namespace Geta.EmailNotification.Shared
{
    /// <summary>
    /// Renders an email view.
    /// </summary>
    public interface IEmailViewRenderer
    {
        /// <summary>
        /// Renders an email view based on the provided view name.
        /// </summary>
        /// <param name="email">The email data to pass to the view.</param>
        /// <param name="viewName">Optional, the name of the view. If null, the ViewName of the email will be used.</param>
        /// <returns>The string result of rendering the email.</returns>
        string Render(IEmailNotificationRequest email, string viewName = null);

        Task<string> RenderAsync(IEmailNotificationRequest email, string viewName = null);
    }
}