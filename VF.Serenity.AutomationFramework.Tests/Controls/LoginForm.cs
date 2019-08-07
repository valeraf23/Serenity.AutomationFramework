using VF.Serenity.AutomationFramework.Controls;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Tests.Controls
{
    public class LoginForm : HtmlElement
    {
        [FindBy(How.CssSelector, "input[type='email']")]
        public TextBox UserName { get; set; }

        [FindBy(How.Id, "identifierNext")] public Button Next { get; set; }
    }
}