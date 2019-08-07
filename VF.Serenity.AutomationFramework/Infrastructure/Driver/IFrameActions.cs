using System.Linq;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure.Driver
{
    public static class IframeActions
    {
        public static bool CanSwitchToIframeByName(string frameName)
        {
            var iframes = DriverService.Driver.FindElements(new Locator(How.TagName, "iframe"));
            return iframes.Count >= 1 && iframes.Where(iframe => iframe.Displayed)
                       .Any(iframe => iframe.GetAttribute("name").Equals(frameName));
        }
    }
}
