using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using VF.Serenity.AutomationFramework.Infrastructure.Extensions;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure.Driver
{
    public sealed class WebDriver : IWebDriver, IJSExecutor
    {
        private OpenQA.Selenium.IWebDriver _nativeDriver;

        internal OpenQA.Selenium.IWebDriver NativeDriver
        {
            get
            {
                if (!IsRunning)
                    _nativeDriver = InitDriver();
                return _nativeDriver;
            }
        }

        public bool IsRunning => _nativeDriver != null && _nativeDriver.Alive();
        public T GetDriver<T>() => (T) _nativeDriver;
        public T CastTo<T>() => (T)(object)this;

        private static OpenQA.Selenium.IWebDriver InitDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable-web-security", "start-maximized", "--disable-extensions");

            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            options.AddArgument("--safebrowsing-disable-download-protection");
            options.AddArgument("--safebrowsing-disable-extension-blacklist");
            options.AddArgument("--safebrowsing-disable-auto-update");
            options.AddArgument("--safebrowsing-manual-download-blacklist");
            options.AddArgument("--allow-unchecked-dangerous-downloads");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-running-insecure-content");
            options.AddArgument("--allow-insecure-localhost");
            options.AddArgument("--disable-popup-blocking");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", false);
            var nativeDriver = new ChromeDriver(GetChromeDriverService(), options);
            nativeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            nativeDriver.Manage().Window.Maximize();
            return nativeDriver;
        }

        private static ChromeDriverService GetChromeDriverService()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            chromeDriverService.HideCommandPromptWindow = true;
            return chromeDriverService;
        }

        IWebElement INative.FindElement(Locator locator, int index) =>
            NativeDriver.FindElement(locator.ToSeleniumLocator());

        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) =>
            NativeDriver.FindElements(locator.ToSeleniumLocator());

        public void Get(string url) => NativeDriver.Navigate().GoToUrl(url);

        public void Close() => _nativeDriver?.Quit();

        public string Url => NativeDriver.Url;

        public string Title => NativeDriver.Title;

        public void TakeScreenshot(string fileName) => ((ITakesScreenshot) NativeDriver).GetScreenshot()
            .SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);

        void IJSExecutor.ExecuteScript(string script, params object[] args) =>
            NativeDriver.ExecuteJavaScript(script, args);

        T IJSExecutor.ExecuteScript<T>(string script, params object[] args) =>
            NativeDriver.ExecuteJavaScript<T>(script, args);

        public void Dispose() => _nativeDriver?.Quit();

        public void Refresh() => NativeDriver.Navigate().Refresh();
    }
}