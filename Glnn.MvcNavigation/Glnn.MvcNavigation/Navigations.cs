using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Glnn.MvcNavigation.Entities.Impl;
using Glnn.MvcNavigation.FluentConfiguration.Impl;
using Glnn.MvcNavigation.FluentDisplay;
using System.Web.Mvc.Html;
using Glnn.MvcNavigation.FluentDisplay.Impl;

namespace Glnn.MvcNavigation
{
    public static class Navigations
    {
        private static readonly IDictionary<string, Navigation> NavigationsContainer;

        static Navigations()
        {
            NavigationsContainer = new Dictionary<string, Navigation>();
        }

        public static FluentConfiguration.IRootNavigationConfiguration CreateNew(string name, Type resourceType)
        {
            var navigation = new Navigation(resourceType);
            NavigationsContainer.Add(name.ToLower(), navigation);
            return new FluentConfigurator(navigation);
        }

        public static IFluentDisplay Navigation(this HtmlHelper htmlHelper, string name)
        {
            var securityHelper = new ActionPermissionsHelper(htmlHelper);

            var routeData = htmlHelper.ViewContext.RouteData;

            var activeTarget = new Target(routeData.GetRequiredString("controller"),
                                          routeData.GetRequiredString("action"));

            var navigation = NavigationsContainer[name.ToLower()];
            navigation.ActiveTarget = activeTarget;
            return new FluentDisplay.Impl.FluentDisplay(securityHelper, navigation);
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, INavigationItem navigationItem)
        {
            var cssClasses = navigationItem.CssClasses.ToList();

            if(navigationItem.IsActive) cssClasses.Add("active");
            return htmlHelper.ActionLink(
                navigationItem.Name,
                navigationItem.Target.Action,
                navigationItem.Target.Controller,
                null,
                new
                    {
                        @class = string.Join(" ", cssClasses.Distinct())
                    });
        }

        public static IEnumerable<MvcHtmlString> ActionLinks(this HtmlHelper htmlHelper, IEnumerable<INavigationItem> navigationItems)
        {
            return navigationItems.Select(navigationItem => htmlHelper.ActionLink(navigationItem));
        }

        public static MvcHtmlString ActionLinks(this HtmlHelper htmlHelper, string separator, IEnumerable<INavigationItem> navigationItems)
        {
            return new MvcHtmlString(string.Join(separator, htmlHelper.ActionLinks(navigationItems)));
        }

        public static MvcHtmlString ActionLinks(this HtmlHelper htmlHelper, string separator, IEnumerable<INavigationItem> navigationItems, string format)
        {
            var formatted = navigationItems.Select(x => string.Format(format, htmlHelper.ActionLink(x)));
            return new MvcHtmlString(string.Join(separator, formatted));
        }
    }
}
