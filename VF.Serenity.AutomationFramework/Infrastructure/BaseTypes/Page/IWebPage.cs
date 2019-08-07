namespace VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Page
{
    public interface IWebPage : ISearchable, INative
    {
        string Address { get; set; }
        void Open();
        string Title { get; set; }
        string Url { get; set; }
        void Refresh();
        void WaitForReady();
        void ScrollDown();
    }
}
