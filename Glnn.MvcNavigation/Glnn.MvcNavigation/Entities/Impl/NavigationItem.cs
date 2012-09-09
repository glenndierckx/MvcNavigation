using System;
using System.Collections.Generic;
using System.Linq;
using Glnn.MvcNavigation.Helpers;

namespace Glnn.MvcNavigation.Entities.Impl
{
    class NavigationItem : INavigationItem
    {
        private readonly string _name;
        private readonly Type _resourceType;
        private readonly Func<Target> _activeTarget;
        private readonly IList<NavigationItem> _subNavigation;
        private IEnumerable<Target> _activeOnTargets;
        private readonly IList<Target> _predefinedActiveOnTargets;
        private readonly IList<string> _cssClasses;
        private readonly IList<Func<string>> _calculatedCssClasses;

        public NavigationItem(string name, Type resourceType, Target target, Func<Target> activeTarget)
        {
            _subNavigation = new List<NavigationItem>();
            _name = name;
            _resourceType = resourceType;
            _activeTarget = activeTarget;
            _predefinedActiveOnTargets = new List<Target>();
            _cssClasses = new List<string>();
            _calculatedCssClasses = new List<Func<string>>();
            Target = target;
        }

        public Target Target { get; private set; }
        public IEnumerable<INavigationItem> Children { get { return _subNavigation; } }
        public void AlsoActiveOn(string controller)
        {
            _predefinedActiveOnTargets.Add(new Target(controller));
        }
        public void AlsoActiveOn(string controller, string action)
        {
            _predefinedActiveOnTargets.Add(new Target(controller, action));
        }

        public void AddCssClass(string cssClass)
        {
            _cssClasses.Add(cssClass);
        }

        public void AddCssClass(Func<string> cssClass)
        {
            _calculatedCssClasses.Add(cssClass);
        }

        public IEnumerable<string> CssClasses 
        { 
            get
            {
                foreach (var cssClass in _cssClasses)
                {
                    yield return cssClass;
                }
                foreach (var calculatedCssClass in _calculatedCssClasses)
                {
                    yield return calculatedCssClass();
                }
            }
        }

        public string Name
        {
            get { return _resourceType == null ? _name : ResourceHelper.Get(_resourceType, _name) ?? _name; } // todo : get from resource
        }
        private IEnumerable<Target> ActiveOnTargets
        {
            get
            {
                if (_activeOnTargets == null)
                {
                    var activeOnTargets = new List<Target> { Target };
                    activeOnTargets.AddRange(_predefinedActiveOnTargets);
                    foreach (var navigationItem in _subNavigation)
                    {
                        activeOnTargets.AddRange(navigationItem.ActiveOnTargets);
                    }
                    _activeOnTargets = activeOnTargets;
                }
                return _activeOnTargets;
            }
        }
        public bool IsActive
        {
            get
            {
                var activeTarget = _activeTarget();
                return ActiveOnTargets.Any(x => x == activeTarget);
            }
        }
        public INavigationItem AddNavigationItem(string name, Target target)
        {
            var navigationItem = new NavigationItem(name, _resourceType, target, _activeTarget);
            _subNavigation.Add(navigationItem);
            return navigationItem;
        }
    }
}