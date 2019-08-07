using System;
using System.Linq;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;

namespace VF.Serenity.AutomationFramework.Infrastructure.Extensions
{
    public static class WebElementMethods
    {
        public static bool HasClass(this IHtmlElement element, string className)
        {
            var classValue = element.GetProperty("class");
            var classes = classValue.Split(' ');
            return classes.Any(i => i.Equals(className, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}