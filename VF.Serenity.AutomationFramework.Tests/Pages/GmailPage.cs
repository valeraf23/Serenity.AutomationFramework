using VF.Serenity.AutomationFramework.Controls;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Collections;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Page;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;
using VF.Serenity.AutomationFramework.Tests.Controls;

namespace VF.Serenity.AutomationFramework.Tests.Pages
{
    internal class GmailPage: WebPage
    {
        [FindBy(How.CssSelector, ".T-I.J-J5-Ji.T-I-KE.L3")]
        public Button CreateButton { get; set; }

        [FindBy(How.ClassName, "AD")]
        public CreateMailForm CreateMailForm { get; set; }

        [FindBy(How.CssSelector, ".F.cf.zt tr")]
        public HtmlElementsCollection<MailRow> MailRows { get; set; }
    }
}