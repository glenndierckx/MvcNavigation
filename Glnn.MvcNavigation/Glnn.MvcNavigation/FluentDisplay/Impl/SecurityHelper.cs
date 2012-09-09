using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Glnn.MvcNavigation.FluentDisplay.Impl
{
    static class SecurityHelper
    {
        internal static bool HasActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            var controllerToLinkTo = string.IsNullOrEmpty(controllerName)
                ? htmlHelper.ViewContext.Controller
                : GetControllerByName(htmlHelper, controllerName);

            var controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerToLinkTo);

            var controllerDescriptor = new ReflectedControllerDescriptor(controllerToLinkTo.GetType());
            var actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            return ActionIsAuthorized(controllerContext, actionDescriptor);
        }

        static bool ActionIsAuthorized(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
                return false;

            var authContext = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (var authFilter in FilterProviders.Providers.GetFilters(authContext, actionDescriptor).Where(authFilter => authFilter.Instance is AuthorizeAttribute))
            {
                ((IAuthorizationFilter)authFilter.Instance).OnAuthorization(authContext);

                if (authContext.Result != null)
                    return false;
            }

            return true;
        }

        static ControllerBase GetControllerByName(HtmlHelper helper, string controllerName)
        {
            var factory = ControllerBuilder.Current.GetControllerFactory();

            var controller = factory.CreateController(helper.ViewContext.RequestContext, controllerName);

            if (controller == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentUICulture,
                        "Controller factory {0} controller {1} returned null",
                        factory.GetType(),
                        controllerName));
            }

            return (ControllerBase)controller;
        }
    }
}
