using System;
using Glnn.MvcNavigation.Entities;
using Glnn.MvcNavigation.Entities.Impl;

namespace Glnn.MvcNavigation.FluentConfiguration.Impl
{
    class FluentConfigurator : IRootNavigationItemActions, IRootNavigationConfiguration
    {
        private readonly Navigation _navigation;
        private INavigationItem _lastAddedNavigationItem;

        public FluentConfigurator(Navigation navigation)
        {
            _navigation = navigation;
        }

        public IRootNavigationItemActions AddNavigationItem(string name, string controller, string action)
        {
            _lastAddedNavigationItem = _navigation.AddNavigationItem(name, new Target(controller, action));
            return this;
        }

        public ISubNavigationConfiguration<IRootNavigationConfiguration> BeginSubnavigation()
        {
            if (_lastAddedNavigationItem == null) throw new InvalidOperationException();
            return new FluentSubNavigationConfigurator<IRootNavigationConfiguration>(this, _lastAddedNavigationItem);
        }

        public IRootNavigationItemActions AlsoActiveOn(string controller)
        {
            _lastAddedNavigationItem.AlsoActiveOn(controller);
            return this;
        }

        public IRootNavigationItemActions AlsoActiveOn(string controller, string action)
        {
            _lastAddedNavigationItem.AlsoActiveOn(controller, action);
            return this;
        }

        public IRootNavigationItemActions AddCssClass(string cssClass)
        {
            _lastAddedNavigationItem.AddCssClass(cssClass);
            return this;
        }

        public IRootNavigationItemActions AddCssClass(Func<string> cssClass)
        {
            _lastAddedNavigationItem.AddCssClass(cssClass);
            return this;
        }
    }
}
