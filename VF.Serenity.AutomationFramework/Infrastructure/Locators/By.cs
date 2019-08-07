namespace VF.Serenity.AutomationFramework.Infrastructure.Locators
{
    public static class By
    {
        public static Locator Id(string id) => new Locator(How.Id, id);

        public static Locator XPath(string xpath) => new Locator(How.XPath, xpath);

        public static Locator CssSelector(string cssSelector) => new Locator(How.CssSelector, cssSelector);

        public static Locator TagName(string tagName) => new Locator(How.TagName, tagName);

        public static Locator ClassName(string className) => new Locator(How.ClassName, className);

        public static Locator LinkText(string linkText) => new Locator(How.LinkText, linkText);

        public static Locator Name(string name) => new Locator(How.Name, name);

        public static Locator PartialLinkText(string partialLink) => new Locator(How.PartialLinkText, partialLink);

        // ReSharper disable once InconsistentNaming
        public static Locator jQuery(string selector) => new Locator(How.jQuery, selector);
    }
}