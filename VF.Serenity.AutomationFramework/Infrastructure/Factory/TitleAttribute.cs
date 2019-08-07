using System;

namespace VF.Serenity.AutomationFramework.Infrastructure.Factory
{
    public class TitleAttribute : Attribute
    {
        public string Title { get; }

        public TitleAttribute(string url)
        {
            Title = url;
        }
    }
}