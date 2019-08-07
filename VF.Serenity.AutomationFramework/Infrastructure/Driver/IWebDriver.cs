using System;

namespace VF.Serenity.AutomationFramework.Infrastructure.Driver
{
    public interface IWebDriver : INative, IDisposable
    {
        void Get(string url);
        void Close();
        string Url { get; }
        string Title { get; }
        void TakeScreenshot(string fileName);
        void Refresh();
        bool IsRunning { get; }
        T GetDriver<T>();
        T CastTo<T>();
    }
}
