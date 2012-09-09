namespace Glnn.MvcNavigation.FluentConfiguration
{
    public interface ISubNavigationConfiguration<TParentType>
    {
        ISubNavigationItemActions<TParentType> AddNavigationItem(string name, string controller, string action);
    }
}