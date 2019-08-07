using System;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure.Factory
{
    public class FindByAttribute : Attribute
    {
        public How How { get; }

        public string Using { get; }

        public FindByAttribute(How how, string @using)
        {
            How = how;
            Using = @using;
        }
    }
}
