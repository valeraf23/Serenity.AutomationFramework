using System;
using OpenQA.Selenium;

namespace VF.Serenity.AutomationFramework.Infrastructure.Extensions
{
    internal static class WebElementExtensions
    {
        public static bool Exists(this IWebElement element)
        {
            try
            {
                // Poke element.
                // ReSharper disable once UnusedVariable
                var text = element.Text;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
