using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SmartWait;
using VF.Serenity.AutomationFramework.Configuration;
using VF.Serenity.AutomationFramework.Extensions.EnumerableExtensions;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Collections;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Driver;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;
using VF.Serenity.AutomationFramework.PageReadyWaiter;

namespace VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Page
{
    using DriverService = Driver.DriverService;

    public abstract class WebPage : IWebPage
    {
        public TElement Find<TElement>(Locator locator) where TElement : IHtmlElement, new()
        {
            var element = ElementFactory.Create<TElement>(this, locator);
            return element;
        }

        public IEnumerable<TElement> FindAll<TElement>(Locator locator) where TElement : IHtmlElement, new()
        {
            IHtmlElementsCollection<TElement> elementsCollection = new HtmlElementsCollection<TElement>(this, locator);
            foreach (var element in elementsCollection)
            {
                yield return element;
            }
        }

        IWebElement INative.FindElement(Locator locator, int index) => DriverService.Driver.FindElement(locator, index);

        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) =>
            DriverService.Driver.FindElements(locator);

        public void Open() => DriverService.Driver.Get(_IWebPage.Address);

        string IWebPage.Address { get; set; }

        string IWebPage.Title { get; set; }
        string IWebPage.Url { get; set; }
        private IWebPage _IWebPage => this;
        public virtual bool IsOpened => DriverService.Driver.Url.Contains(_IWebPage.Url);

        public virtual void Refresh() => DriverService.Driver.Refresh();

        public override string ToString() => _IWebPage.Address;

        public virtual void WaitForReady() => WaitForReady(SerenityConfig.Config.GetPageTimeout);

        public void WaitForReady(TimeSpan time)
        {
            var title = _IWebPage.Title;
            var titleErrorMsg = title.IsNotEmpty()
                ? $"Page with title: \"{title}\" was not opened"
                : "Page was not opened --- Add title for this page";
            WaitForReady(time, titleErrorMsg);
        }

        public void WaitForReady(TimeSpan time, string errorMsg)
        {
            if (_IWebPage.Url.IsNotEmpty())
            {
                WaitFor.Condition(() => IsOpened, errorMsg, time);
            }

            WaitFor.Condition(
                () => GetPageReadyValidator().IsPageLoaded(),
                errorMsg, time);
        }

        protected virtual PageReadyValidator GetPageReadyValidator() => new PageReadyValidator();

        public virtual void ScrollDown() => DriverService.Driver.CastTo<IJSExecutor>()
            .ExecuteScript("window.scrollTo(0,document.body.scrollHeight);");
    }
}