using System;
using VF.Serenity.AutomationFramework.AlertHandler;

namespace VF.Serenity.AutomationFramework.PageReadyWaiter
{
    public sealed class PageReadyValidator
    {
        private readonly IPageReadyValidator _custom;

        public PageReadyValidator()
        {
            _custom = new NoneImplementedPageValidator();
        }

        public PageReadyValidator(IPageReadyValidator page) : this() => _custom = page;

        public bool IsPageLoaded()
        {
            if (BaseAlertHandler.IsAlertPresent())
            {
                Console.WriteLine("AlertPresent");
                return true;
            }

            if (!IsPageLoadComplete())
            {
                Console.WriteLine("Page Load Was not Complete");
                return false;
            }

            if (!IsLoadingFinished())
            {
                Console.WriteLine("Spinner is Display");
                return false;
            }

            return true;
        }

        private bool IsLoadingFinished() => _custom.IsPageReady();

        private static bool IsPageLoadComplete() => JavaScriptLibrary.JavaScriptLibrary.IsDomLoaded();
    }
}