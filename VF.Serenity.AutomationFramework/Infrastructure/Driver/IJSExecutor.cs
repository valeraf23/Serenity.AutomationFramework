namespace VF.Serenity.AutomationFramework.Infrastructure.Driver
{
    public interface IJSExecutor
    {
        void ExecuteScript(string script, params object[] args);
        T ExecuteScript<T>(string script, params object[] args);
    }
}