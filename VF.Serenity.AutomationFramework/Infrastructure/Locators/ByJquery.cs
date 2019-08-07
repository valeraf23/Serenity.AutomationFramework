using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using VF.Serenity.AutomationFramework.Infrastructure.Driver;
using IWebDriver = VF.Serenity.AutomationFramework.Infrastructure.Driver.IWebDriver;

namespace VF.Serenity.AutomationFramework.Infrastructure.Locators
{
    internal class ByJquery : OpenQA.Selenium.By
    {
        private readonly string _selector;

        public ByJquery(string selector) => _selector = $"\"{selector}\"";

        public override IWebElement FindElement(ISearchContext context)
        {
            var element = GetJavaScriptObject(context, 0);
            if (element != null) return element;
            throw new NoSuchElementException("No element found with jQuery command: jQuery" + _selector);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var elements = GetJavaScriptObject(context) as ReadOnlyCollection<IWebElement>;
            return elements ?? new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
        }

        private IWebElement GetJavaScriptObject(ISearchContext context, int? index = null) =>
            context is IWebDriver
                ? FindElementsByJQuery(index)
                : FindRelatedElementsByJQuery((IWebElement) context, index);

        private IWebElement FindElementsByJQuery(int? index) =>
            Driver.DriverService.Driver.CastTo<IJSExecutor>().ExecuteScript<IWebElement>(
                $"return jQuery({_selector}).get({index})");

        private IWebElement FindRelatedElementsByJQuery(IWebElement element, int? index) =>
            Driver.DriverService.Driver.CastTo<IJSExecutor>().ExecuteScript<IWebElement>(
                $"return jQuery(arguments[0]).find({_selector}).get({index})", element);
    }
}
