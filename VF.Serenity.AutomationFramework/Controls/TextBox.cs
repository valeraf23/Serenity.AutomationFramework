using System;
using SmartWait;
using VF.Serenity.AutomationFramework.Controls.Abstract;
using VF.Serenity.AutomationFramework.Infrastructure.BaseTypes.Element;

namespace VF.Serenity.AutomationFramework.Controls
{
    public class TextBox : HtmlElement, ITextBox
    {
        public virtual void SetText(string text)
        {
            WaitFor.Condition(() =>
            {
                NativeElement.Clear();
                NativeElement.SendKeys(text);
                return true;
            }, $"Could not set text on {ToString()}", TimeSpan.FromSeconds(15));
        }

        public virtual void Clear()
        {
            WaitFor.Condition(() =>
            {
                NativeElement.Clear();
                return NativeElement.Text.Equals(string.Empty);
            }, $"Could not clear {ToString()}", RetryCount);
        }

        public override string GetText() => GetAttribute("value") ?? string.Empty;
    }
}
