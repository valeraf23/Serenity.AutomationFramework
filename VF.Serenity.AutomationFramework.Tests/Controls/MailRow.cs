using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Tests.Controls
{
    public class MailRow:HtmlElement
    {
        [FindBy(How.CssSelector, ".F.cf.zt tr td:nth-child(6) .bqe")]
        public HtmlElement Subject { get; set; }
    }
}