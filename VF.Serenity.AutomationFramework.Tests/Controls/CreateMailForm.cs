using VF.Serenity.AutomationFramework.Controls;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Infrastructure.Locators;
using VF.Serenity.AutomationFramework.Tests.Models;

namespace VF.Serenity.AutomationFramework.Tests.Controls
{
    public class CreateMailForm : HtmlElement
    {
        [FindBy(How.CssSelector, "textarea[name='to']")]
        public TextBox To { get; set; }
        [FindBy(How.CssSelector, "input[name='subjectbox'")]
        public TextBox Subject { get; set; }
        [FindBy(How.CssSelector, ".Am.Al.editable.LW-avf")]
        public TextBox Body { get; set; }
        [FindBy(How.CssSelector, ".T-I.J-J5-Ji.aoO.v7.T-I-atl.L3[role='button']")] public Button Send { get; set; }

        public void Fill(MailModel model)
        {
            To.SetText(model.To);
            Subject.SetText(model.Subject);
            Body.SetText(model.Body);
        }
    }
}