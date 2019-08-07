using System;

namespace VF.Serenity.AutomationFramework.Configuration
{
    public class SerenitySection
    {
        public string BaseUrl { get; set; }
        public TimeSpan GetExplicitWait => TimeSpan.FromSeconds(ExplicitWait);
        public TimeSpan GetImplicitWait => TimeSpan.FromSeconds(ImplicitWait);
        public TimeSpan GetExistsWait => TimeSpan.FromSeconds(ExistsWait);
        public TimeSpan GetPageTimeout => TimeSpan.FromSeconds(PageTimeout);
        public int ExplicitWait { get; set; }
        public int ImplicitWait { get; set; }
        public int ExistsWait { get; set; }
        public int PageTimeout { get; set; }
        public int RetryCount { get; set; }

        public SerenitySection()
        {

        }
    }
}
