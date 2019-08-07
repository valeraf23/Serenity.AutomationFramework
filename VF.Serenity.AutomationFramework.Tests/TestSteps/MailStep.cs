using System;
using System.Linq;
using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Tests.Controls;
using VF.Serenity.AutomationFramework.Tests.Models;
using VF.Serenity.AutomationFramework.Tests.Pages;

namespace VF.Serenity.AutomationFramework.Tests.TestSteps
{
    public class MailStep
    {
        private GmailPage _gmailPage;

        public MailStep CreateMail(MailModel model)
        {
            _gmailPage = PageFactory.Get<GmailPage>();
            _gmailPage.CreateButton.Click();
            _gmailPage.CreateMailForm.Fill(model);

            return this;
        }

        public MailRow GetMail(Func<MailRow, bool> func) =>
            SmartWait.WaitFor.For(() => _gmailPage.MailRows).Become(x => x.Any(func), "Mail has not found")
                .First();


        public MailStep Send()
        {
            _gmailPage.CreateMailForm.Send.Click();
            return this;
        }
    }
}