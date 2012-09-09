using System.Web.Mvc;

namespace Glnn.MvcNavigation.FluentDisplay.Impl
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