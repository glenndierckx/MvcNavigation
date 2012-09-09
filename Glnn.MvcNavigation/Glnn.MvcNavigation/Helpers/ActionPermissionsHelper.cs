using System.Web.Mvc;
using Glnn.MvcNavigation.FluentDisplay;
using Glnn.MvcNavigation.FluentDisplay.Impl;

namespace Glnn.MvcNavigation.Helpers
{
    class ActionPermissionsHelper : ISecurityHelper
    {
        private readonly HtmlHelper _htmlHelper;

        public ActionPermissionsHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public bool HasAccess(INavigationItem navigationItem)
        {
            return _htmlHelper.HasActionPermission(navigationItem.Target.Action, navigationItem.Target.Controller);
        }
    }
}