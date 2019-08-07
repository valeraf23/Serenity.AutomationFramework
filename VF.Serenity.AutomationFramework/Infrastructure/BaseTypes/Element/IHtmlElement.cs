using OpenQA.Selenium;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;

namespace VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element
{
    public interface IHtmlElement : ISearchable, INative
    {
        void SetNativeElement(IWebElement nativeElement);
        IWebElement GetNativeElement();
        void Click();
        string GetText();
        bool Displayed { get; }
        bool Exists { get; }
        bool Enabled { get; }
        SearchStrategy SearchStrategy { get; set; }
        INative Parent { get; set; }
        string GetAttribute(string property);
        string GetProperty(string property);
    }
}
