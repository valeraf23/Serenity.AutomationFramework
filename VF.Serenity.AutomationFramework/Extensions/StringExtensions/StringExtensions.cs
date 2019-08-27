using System;
using System.Collections.Generic;
using System.Linq;
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
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ012345678";
            return GenerateString(length, chars);
        }

        public static string GenerateString(int length, string chars)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
