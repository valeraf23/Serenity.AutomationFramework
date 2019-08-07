using Microsoft.Extensions.DependencyInjection;
using VF.Serenity.AutomationFramework.Configuration;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Page;

namespace VF.Serenity.AutomationFramework.Infrastructure.Factory
{
    public static class PageFactory
    {
        public static TPage Get<TPage>() where TPage : IWebPage, new()
        {
            IWebPage page = new TPage();
            InitPage(page);
            return (TPage) page;
        }

        private static void InitPage(IWebPage page)
        {
            if (page.GetType().HasUrlAttribute())
            {
                page.Url = page.GetType().GetUrlAttribute().Url;
                page.Address = TestsBootstrap.Instance.ServiceProvider.GetService<SerenitySection>().BaseUrl +
                               page.Url;
            }

            if (page.GetType().HasTitleAttribute())
                page.Address = page.GetType().GetTitleAttribute().Title;

            ElementFactory.InitProperties(page);
        }
    }
}
