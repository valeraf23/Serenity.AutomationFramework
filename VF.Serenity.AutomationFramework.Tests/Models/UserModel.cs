namespace VF.Serenity.AutomationFramework.Tests.Models
{
    public class UserModel
    {
        public readonly string Name;
        public readonly string Password;

        public UserModel(string name, string password)
        {
            Name = name;
            Password = password;
        }      
    }
}