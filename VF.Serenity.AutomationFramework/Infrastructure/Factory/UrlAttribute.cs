using System;

namespace VF.Serenity.AutomationFramework.Infrastructure.Factory
{
    public class UrlAttribute : Attribute
    {
        public string Url { get; }

        public UrlAttribute(string url)
        {
            Url = url;
        }
    }
}