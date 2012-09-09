using Glnn.MvcNavigation.Entities.Impl;

namespace Glnn.MvcNavigation
{
    public interface IAddNavigationItem
    {
        INavigationItem AddNavigationItem(string name, Target target);
    }
}
