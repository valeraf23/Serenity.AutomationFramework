namespace VF.Serenity.AutomationFramework.AlertHandler
{
    public sealed class AlertHandler : BaseAlertHandler
    {
        public static void AcceptAlert() => HandleAlert(alert => alert.Accept());

        public static void DismissAlert() => HandleAlert(alert => alert.Dismiss());
    }
}