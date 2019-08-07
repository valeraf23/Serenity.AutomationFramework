using VF.Serenity.AutomationFramework.Infrastructure.Locators;

namespace VF.Serenity.AutomationFramework.Infrastructure.Factory
{
    public class SearchStrategy
    {
        public SearchStrategy(Locator locator)
        {
            Locator = locator;
        }

        public SearchStrategy(Locator locator, int index)
        {
            Locator = locator;
            Index = index;
        }

        public SearchStrategy()
        {
        }

        public Locator Locator { get; }
        public int Index { get; }
    }
}
