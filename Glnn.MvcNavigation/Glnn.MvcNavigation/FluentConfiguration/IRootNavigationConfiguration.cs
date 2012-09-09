namespace Glnn.MvcNavigation.FluentConfiguration
{
    public interface IRootNavigationConfiguration
    {
        IRootNavigationItemActions AddNavigationItem(string name, string controller, string action);
    }
}