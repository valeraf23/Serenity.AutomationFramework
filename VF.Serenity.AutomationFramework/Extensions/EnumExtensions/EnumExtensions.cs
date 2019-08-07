using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VF.Serenity.AutomationFramework.Extensions.EnumExtensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get string value from DisplayAttribute Name property
        /// </summary>
        /// <param name="this"></param>
        /// <returns> string value</returns>
        public static string ToDisplayString(this Enum @this)
        {
            var attributes = (DisplayAttribute[]) @this.GetType().GetField(@this.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : @this.ToString();
        }

        /// <summary>
        /// Convert string to enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value which we want convert to enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NullReferenceException();
            }

            if (Enum.TryParse(Regex.Replace(value, @"\s+", string.Empty), true, out T result))
            {
                return result;
            }

            throw new InvalidCastException($"Cannot convert string \"{value}\" to {typeof(T).Name}");
        }

    }
}
