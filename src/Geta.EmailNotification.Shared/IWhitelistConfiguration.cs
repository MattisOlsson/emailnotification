namespace Geta.EmailNotification.Shared
{
    public interface IWhitelistConfiguration
    {
        string[] Domains { get; }
        string[] Emails { get; }
        bool HasWhitelist { get; }
    }
}