using VF.Serenity.AutomationFramework.Infrastructure.Factory;
using VF.Serenity.AutomationFramework.Tests.Models;
using VF.Serenity.AutomationFramework.Tests.Pages;

namespace VF.Serenity.AutomationFramework.Tests.TestSteps
{
    public class LoginStep
    {
        private LoginPage _loginPage;
        private PasswordPage _passwordPage;

        public void LoginAs(UserModel user)
        {
            _loginPage = PageFactory.Get<LoginPage>();
            _loginPage.Open();
            _loginPage.LoginForm.UserName.SetText(user.Name);
            _loginPage.LoginForm.Next.Click();

            _passwordPage = PageFactory.Get<PasswordPage>();
            _passwordPage.WaitForReady();
            _passwordPage.PasswordForm.Password.SetText(user.Password);
            _passwordPage.PasswordForm.Next.Click();
        }
    }
}