using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Page;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;
using VF.Serenity.AutomationFramework.Tests.Controls;

namespace VF.Serenity.AutomationFramework.Tests.Pages
{
    [Url("/")]
    class LoginPage : WebPage
    {
        [FindBy(How.Id, "view_container")]
        public LoginForm LoginForm { get; set; }
    }
}