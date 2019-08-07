using VF.Serenity.AutomationFramework.Controls;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Tests.Controls
{
    class PasswordForm : HtmlElement
    {
        [FindBy(How.CssSelector, "input[type='password']")]
        public TextBox Password { get; set; }

        [FindBy(How.Id, "passwordNext")]
        public Button Next { get; set; }
    }
}