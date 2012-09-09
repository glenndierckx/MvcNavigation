using System;
using System.Collections.Generic;

namespace Glnn.MvcNavigation.Entities.Impl
{
    public class Navigation : INavigation
    {
        private readonly IList<INavigationItem> _navigationItems;
        private readonly Type _resourceType;

        public Navigation(Type resourceType)
        {
            _navigationItems = new List<INavigationItem>();
            _resourceType = resourceType;
        }

        public IEnumerable<INavigationItem> NavigationItems { get { return _navigationItems; } }

        public Target ActiveTarget { get; set; }

        public INavigationItem AddNavigationItem(string name, Target target)
        {
            var item = new NavigationItem(name, _resourceType, target, () => ActiveTarget);
            _navigationItems.Add(item);
            return item;
        }
    }
}
