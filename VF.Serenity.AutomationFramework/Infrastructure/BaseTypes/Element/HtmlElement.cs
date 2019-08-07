using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SmartWait;
using SmartWait.ExceptionHandler;
using VF.Serenity.AutomationFramework.Configuration;
using VF.Serenity.AutomationFramework.Extensions.EnumerableExtensions;
using VF.Serenity.AutomationFramework.Helperes;
using VF.Serenity.AutomationFramework.Infrastructure.Driver;
using VF.Serenity.AutomationFramework.Infrastructure.Extensions;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element
{
    public class HtmlElement : IHtmlElement
    {
        protected int RetryCount = SerenityConfig.Config.RetryCount;
        private IWebElement _nativeElement;

        protected IWebElement NativeElement
        {
            get
            {
                return WaitFor.For(ReInitNativeElement,
                        w => w.SetExceptionHandling(ExceptionHandling.Collect)
                            .SetMaxWaitTime(SerenityConfig.Config.GetExplicitWait).Build())
                    .Become(x => x.Exists(), $"Can not find element {this}");
            }
        }

        private IWebElement ReInitNativeElement()
        {
            if (_nativeElement == null || !_nativeElement.Exists())
                _nativeElement = Parent.FindElement(SearchStrategy.Locator, SearchStrategy.Index);
            return _nativeElement;
        }

        public SearchStrategy SearchStrategy { get; set; }

        #region Navigation

        public INative Parent { get; set; }

        public TElement Find<TElement>(Locator locator) where TElement : IHtmlElement, new()
        {
            var element = ElementFactory.Create<TElement>(this, locator);
            return element;
        }

        public IEnumerable<TElement> FindAll<TElement>(Locator locator) where TElement : IHtmlElement, new()
        {
            var elementsCollection = CollectionFactory.Create<TElement>(this, locator);
            foreach (var element in elementsCollection)
            {
                yield return element;
            }
        }

        IWebElement INative.FindElement(Locator locator, int index) =>
            NativeElement.FindElement(locator.ToSeleniumLocator());

        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) =>
            NativeElement.FindElements(locator.ToSeleniumLocator());

        #endregion

        #region Actions

        /// <summary>
        /// Attribute computed by browser (browser tool)
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string GetAttribute(string attribute) => Do.Retry(() => NativeElement.GetAttribute(attribute));

        /// <summary>
        /// Property you can read from HHML element
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetProperty(string property)
        {
            var propertyValue =
                Do.Retry<string>(
                    () => Driver.DriverService.Driver.CastTo<IJSExecutor>()
                        .ExecuteScript<string>("return arguments[0].getAttribute(arguments[1]);", NativeElement,
                            property), RetryCount);
            return propertyValue?.ToString();
        }

        public virtual bool Displayed => Do.Retry(() => Exists && NativeElement.Displayed, RetryCount);

        public virtual bool Exists
        {
            get
            {
                try
                {
                    // Poke element.
                    // ReSharper disable once UnusedVariable
                    var text = ReInitNativeElement().Text;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public virtual bool Enabled => Do.Retry(() => Displayed && NativeElement.Enabled, RetryCount);

        void IHtmlElement.SetNativeElement(IWebElement nativeElement) => _nativeElement = nativeElement;

        IWebElement IHtmlElement.GetNativeElement() => NativeElement;

        public virtual void Click()
        {
            Do.Retry(() =>
            {
                try
                {
                    NativeElement.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (InvalidOperationException e)
                {
                    if (e.Message.Contains("is not clickable at point"))
                        return false;
                    throw;
                }
            });
        }

        public virtual string GetText() => WaitFor.For(() => NativeElement.Text)
            .Become(x => x.IsNotEmpty(), $"Could not get text from {ToString()}");

        public override string ToString()
        {
            var elementName = GetType().Name;
            var locator = SearchStrategy.Locator.ToString();
            return $"{elementName}. {locator}";
        }

        #endregion
    }
}