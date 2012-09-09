using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Glnn.MvcNavigation.Entities.Impl;

namespace Glnn.MvcNavigation
{
    public interface INavigationItem : IAddNavigationItem
    {
        string Name { get; }
        Target Target { get; }
        bool IsActive { get; }
        IEnumerable<INavigationItem> Children { get; }
        void AlsoActiveOn(string controller);
        void AlsoActiveOn(string controller, string action);

        void AddCssClass(string cssClass);
        void AddCssClass(Func<string> cssClass);

        IEnumerable<string> CssClasses { get; }
    }
}