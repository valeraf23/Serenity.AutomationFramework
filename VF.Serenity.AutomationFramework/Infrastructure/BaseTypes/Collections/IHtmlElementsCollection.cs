using System.Collections.Generic;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;

namespace VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Collections
{
    public interface IHtmlElementsCollection<out THtmlElement>: IEnumerable<THtmlElement> where THtmlElement: IHtmlElement, new()
    {
    }
}