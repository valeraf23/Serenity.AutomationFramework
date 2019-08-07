using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;

namespace VF.Serenity.AutomationFramework.Controls.Abstract
{
    public interface ITextBox : IHtmlElement
    {
        void SetText(string text);
        void Clear();
    }
}