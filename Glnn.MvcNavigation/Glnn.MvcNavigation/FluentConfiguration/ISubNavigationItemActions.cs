using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glnn.MvcNavigation.FluentConfiguration
{
    public interface ISubNavigationItemActions<TParentType> : ISubNavigationConfiguration<TParentType>
    {
        ISubNavigationConfiguration<ISubNavigationConfiguration<TParentType>> BeginSubnavigation();
        ISubNavigationItemActions<TParentType> AlsoActiveOn(string controller);
        ISubNavigationItemActions<TParentType> AlsoActiveOn(string controller, string action);
        ISubNavigationItemActions<TParentType> AddCssClass(string cssClass);
        ISubNavigationItemActions<TParentType> AddCssClass(Func<string> cssClass);
        TParentType EndSubnavigation();
    }
}
