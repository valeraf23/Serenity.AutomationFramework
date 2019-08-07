using System;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Collections;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure.Factory
{
    internal static class CollectionFactory
    {
        internal static IHtmlElementsCollection<THtmlElement> Create<THtmlElement>(INative parent, Locator locator)
            where THtmlElement : IHtmlElement, new() => new HtmlElementsCollection<THtmlElement>(parent, locator);

        internal static object Create(Type type, INative parent, Locator locator)
        {
            var listType = typeof(HtmlElementsCollection<>);
            var genericArgs = type.GetGenericArguments();
            var concreteType = listType.MakeGenericType(genericArgs);
            var newList = Activator.CreateInstance(concreteType, parent, locator);
            return newList;
        }
    }
}