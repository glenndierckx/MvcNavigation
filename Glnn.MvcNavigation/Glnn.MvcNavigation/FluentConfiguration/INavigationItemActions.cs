namespace Glnn.MvcNavigation.FluentConfiguration
{
    public interface INavigationItemActions<TSubNavType>
    {
        ISubNavigationConfiguration<IRootNavigationConfiguration> BeginSubnavigation();
    }
}