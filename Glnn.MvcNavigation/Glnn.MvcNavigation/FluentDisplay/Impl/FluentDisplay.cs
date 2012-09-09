using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Glnn.MvcNavigation.Entities;
using Glnn.MvcNavigation.Entities.Impl;

namespace Glnn.MvcNavigation.FluentDisplay.Impl
{
    public class FluentDisplay : IFluentDisplay
    {
        private readonly ISecurityHelper _securityHelper;
        private readonly Navigation _navigation;
        private int _levelIndex;
        private bool _keepDisplaying;
        private readonly IList<Func<string>> _navigationLevels;

        public FluentDisplay(ISecurityHelper securityHelper,Navigation navigation)
        {
            _navigationLevels = new List<Func<string>>();
            _securityHelper = securityHelper;
            _navigation = navigation;
            _keepDisplaying = true;
        }

        public IFluentDisplay Display(Func<IEnumerable<INavigationItem>, string> displayRow)
        {
            if(_keepDisplaying)
            {
                var items = GetNavigationItemsAtLevel(_levelIndex);
                if (items != null)
                {
                    var itemsWithAccess = items.Where(_securityHelper.HasAccess).ToList();
                    if(itemsWithAccess.Any())
                    {
                        _navigationLevels.Add(() => displayRow(itemsWithAccess));
                    }
                    else
                    {
                        _keepDisplaying = false;
                    }
                }
                else
                {
                    _keepDisplaying = false;
                }
                _levelIndex++;
            }
            return this;
        }

        private IEnumerable<INavigationItem> GetNavigationItemsAtLevel(int levelIndex)
        {
            var navigationItemsFounds = _navigation.NavigationItems;

            for (int level = 0; level < levelIndex; level++)
            {
                if (level == levelIndex) return navigationItemsFounds;
                var activeItem = _navigation.NavigationItems.SingleOrDefault(x => x.IsActive);
                if (activeItem == null || activeItem.Children == null || !activeItem.Children.Any()) return null;
                navigationItemsFounds = activeItem.Children;
            }

            return navigationItemsFounds;
        }

        public IFluentDisplay SetNavigationLevel(int level)
        {
            _levelIndex = level;
            return this;
        }

        public MvcHtmlString ToHtml()
        {
            return new MvcHtmlString(string.Join("\r\n", _navigationLevels.Select(x => x())));
        }
    }
}
