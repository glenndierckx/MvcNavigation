using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glnn.MvcNavigation
{
    public interface INavigation : IAddNavigationItem
    {
        IEnumerable<INavigationItem> NavigationItems { get; }
    }
}
