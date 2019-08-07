using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VF.Serenity.AutomationFramework.Extensions.StringExtensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string input) => !string.IsNullOrEmpty(input);

        public static string ReplaceWhitespaces(this string input) => ReplaceWhitespaces(input, string.Empty);

        public static string ReplaceWhitespaces(this string input, string replaceWith) =>
            Regex.Replace(input, @"\s+", replaceWith);

        public static string ReplaceNewline(this string input) => ReplaceNewline(input, string.Empty);

        public static string ReplaceNewline(this string input, string replaceWith) =>
            Regex.Replace(input, @"[\r\n]+", replaceWith);

        public static bool Contains(this string source, string toCheck, StringComparison comp) =>
            source.IndexOf(toCheck, comp) >= 0;

        public static IList<string> GetUrlLinks(this string text)
        {
            var matches = new Regex(
                    @"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?")
                .Matches(text);
            return (from Match match in matches select match.Value).ToList();
        }

        public static string GenerateString(int length)
        {

            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 1; i < length + 1; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
