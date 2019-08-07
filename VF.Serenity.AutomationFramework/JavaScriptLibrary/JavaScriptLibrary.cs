using VF.Serenity.AutomationFramework.Infrastructure.Driver;

namespace VF.Serenity.AutomationFramework.JavaScriptLibrary
{
    public static class JavaScriptLibrary
    {
        private static IJSExecutor IjsExecutor
            => DriverService.Driver.CastTo<IJSExecutor>();

        public static bool IsDomLoaded()
        {
            const string func = "return document.readyState==='complete';";
            return IjsExecutor.ExecuteScript<bool>(func);
        }
    }
}