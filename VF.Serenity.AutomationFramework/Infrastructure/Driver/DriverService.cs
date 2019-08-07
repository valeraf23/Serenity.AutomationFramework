using System;

namespace VF.Serenity.AutomationFramework.Infrastructure.Driver
{
    public static class DriverService
    {
        private static readonly Lazy<WebDriver> DriverInstance = new Lazy<WebDriver>();
        public static IWebDriver Driver => DriverInstance.Value;
    }
}
