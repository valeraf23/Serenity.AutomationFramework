namespace VF.Serenity.AutomationFramework.Infrastructure.Locators
{
    public class Locator
    {
        internal Locator(How how, string @using)
        {
            How = how;
            Using = @using;
        }

        public How How { get; }
        public string Using { get;}

        public override string ToString() => $"{How}, {Using}";
    }
}