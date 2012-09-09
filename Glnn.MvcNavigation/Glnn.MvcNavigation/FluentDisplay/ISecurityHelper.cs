namespace Glnn.MvcNavigation.FluentDisplay
{
    public interface ISecurityHelper
    {
        bool HasAccess(INavigationItem navigationItem);
    }
}