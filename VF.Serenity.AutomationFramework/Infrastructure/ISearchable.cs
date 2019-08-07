using System.Collections.Generic;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure
{
    public interface ISearchable
    {
        TElement Find<TElement>(Locator locator) where TElement : IHtmlElement, new();
        IEnumerable<TElement> FindAll<TElement>(Locator locator) where TElement : IHtmlElement, new();
    }
}
