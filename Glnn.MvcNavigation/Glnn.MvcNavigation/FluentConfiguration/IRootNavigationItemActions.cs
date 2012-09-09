using System;

namespace Glnn.MvcNavigation.FluentConfiguration
{
    public interface IRootNavigationItemActions : IRootNavigationConfiguration
    {
        ISubNavigationConfiguration<IRootNavigationConfiguration> BeginSubnavigation();
        IRootNavigationItemActions AlsoActiveOn(string controller);
        IRootNavigationItemActions AlsoActiveOn(string controller, string action);
        IRootNavigationItemActions AddCssClass(string cssClass);
        IRootNavigationItemActions AddCssClass(Func<string> cssClass);
    }
}