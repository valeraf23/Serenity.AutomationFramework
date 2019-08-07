using System;

namespace VF.Serenity.AutomationFramework.PageReadyWaiter
{
    internal class NoneImplementedPageValidator : IPageReadyValidator
    {
        public bool IsPageReady()
        {
            Console.WriteLine("None Implemented Page Validator");
            return true;
        }
    }
}