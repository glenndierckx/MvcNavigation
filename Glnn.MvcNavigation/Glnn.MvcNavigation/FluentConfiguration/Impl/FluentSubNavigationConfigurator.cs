using System;
using Glnn.MvcNavigation.Entities.Impl;

namespace Glnn.MvcNavigation.FluentConfiguration.Impl
{
    class FluentSubNavigationConfigurator<TParentType> : ISubNavigationItemActions<TParentType>
    {
        private readonly TParentType _parentInstance;
        private readonly INavigationItem _navigationItem;
        private INavigationItem _lastAddedNavigationItem;

        public FluentSubNavigationConfigurator(TParentType parentInstance, INavigationItem navigationItem)
        {
            _parentInstance = parentInstance;
            _navigationItem = navigationItem;
        }

        public ISubNavigationConfiguration<ISubNavigationConfiguration<TParentType>> BeginSubnavigation()
        {
            return new FluentSubNavigationConfigurator<ISubNavigationConfiguration<TParentType>>(this,
                                                                                               _lastAddedNavigationItem);
        }

        public ISubNavigationItemActions<TParentType> AlsoActiveOn(string controller)
        {
            _lastAddedNavigationItem.AlsoActiveOn(controller);
            return this;
        }

        public ISubNavigationItemActions<TParentType> AlsoActiveOn(string controller, string action)
        {
            _lastAddedNavigationItem.AlsoActiveOn(controller, action);
            return this;
        }

        public ISubNavigationItemActions<TParentType> AddCssClass(string cssClass)
        {
            _lastAddedNavigationItem.AddCssClass(cssClass);
            return this;
        }

        public ISubNavigationItemActions<TParentType> AddCssClass(Func<string> cssClass)
        {
            _lastAddedNavigationItem.AddCssClass(cssClass);
            return this;
        }

        public TParentType EndSubnavigation()
        {
            return _parentInstance;
        }

        public ISubNavigationItemActions<TParentType> AddNavigationItem(string name, string controller, string action)
        {
            _lastAddedNavigationItem = _navigationItem.AddNavigationItem(name, new Target(controller, action));
            return this;
        }
    }
}
