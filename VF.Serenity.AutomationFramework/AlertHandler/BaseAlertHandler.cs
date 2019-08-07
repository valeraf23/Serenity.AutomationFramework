using System;
using System.Threading;
using OpenQA.Selenium;
using SmartWait;
using SmartWait.ExceptionHandler;
using DriverService = VF.Serenity.AutomationFramework.Infrastructure.Driver.DriverService;

namespace VF.Serenity.AutomationFramework.AlertHandler
{
    public class BaseAlertHandler
    {
        protected BaseAlertHandler()
        {
        }

        private static IWebDriver WebDriver => DriverService.Driver.GetDriver<IWebDriver>();

        public static bool IsAlertPresent()
        {
            try
            {
                return ReturnAlert() != null;
            }
            catch (UnhandledAlertException)
            {
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        private static IAlert ReturnAlert()
        {
            var alert = WebDriver.SwitchTo().Alert();
            return alert;
        }

        public static void HandleAlert(Action<IAlert> action)
        {
            WaitAlert();
            action(ReturnAlert());
        }

        public static string GetAlertText()
        {
            WaitAlert();
            var confirmationGetText = ReturnAlert().Text;
            return confirmationGetText;
        }

        private static void WaitAlert()
        {
            // Sleep for a very short period of time to prevent starving the driver thread.
            Thread.Sleep(250);
            WaitFor.Condition(IsAlertPresent, "Alert did not appear", ExceptionHandling.Collect);
        }
    }
}