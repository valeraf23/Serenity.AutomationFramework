using System.Collections.Generic;
using OpenQA.Selenium;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure
{
    public interface INative
    {
        IWebElement FindElement(Locator locator, int index);
        IReadOnlyCollection<IWebElement> FindElements(Locator locator);
    }
}