using FluentAssertions;
using NUnit.Framework;
using VF.Serenity.AutomationFramework.Infrastructure.Driver;
using VF.Serenity.AutomationFramework.Tests.Models;
using VF.Serenity.AutomationFramework.Tests.TestSteps;

namespace VF.Serenity.AutomationFramework.Tests.Tests
{
    [TestFixture]
    public class Test
    {
        private const string UserName = "";
        private const string Password = "";

        [SetUp]
        public void Precondition()
        {
            Steps.LoginStep.LoginAs(new UserModel(UserName, Password));
        }

        [Test]
        public void Send_Emails()
        {
           
            var mailModel = new MailModel
            {
                To = "valeraf23@gmail.com",
                Subject = "Some",
                Body = "Some text"
            };

            Steps.MailStep
                .CreateMail(mailModel)
                .Send()
                .GetMail(r => r.Subject.GetText() == mailModel.Subject)
                .Subject.GetText()
                .Should()
                .BeEquivalentTo(mailModel.Subject);
        }

        [TearDown]
        public void TearDown() => DriverService.Driver.Close();
    }
}