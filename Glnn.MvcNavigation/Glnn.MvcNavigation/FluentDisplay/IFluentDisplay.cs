using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Glnn.MvcNavigation.FluentDisplay
{
    public interface IFluentDisplay
    {
        IFluentDisplay Display(Func<IEnumerable<INavigationItem>, string> displayRow);
        IFluentDisplay SetNavigationLevel(int level);
        MvcHtmlString ToHtml();
    }
}