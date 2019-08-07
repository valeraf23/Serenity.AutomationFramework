using System;
using Microsoft.Extensions.DependencyInjection;

namespace VF.Serenity.AutomationFramework.Configuration
{
    internal class SerenityConfig
    {
        private static readonly Lazy<SerenitySection> Section =
            new Lazy<SerenitySection>(LoadConfigurationFromAssemblyThatUsingThePlugin);

        private static SerenitySection LoadConfigurationFromAssemblyThatUsingThePlugin() =>
            TestsBootstrap.Instance.ServiceProvider.GetService<SerenitySection>();

        private static void ThrowAnErrorIfConfigurationSectionIsNull()
        {
            if (Section.Value == null)
                throw new Exception("No 'serenity' section in config file!");
        }

        public static SerenitySection Config
        {
            get
            {
                ThrowAnErrorIfConfigurationSectionIsNull();
                return Section.Value;
            }
        }
    }
}
