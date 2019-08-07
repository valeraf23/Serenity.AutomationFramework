using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VF.Serenity.AutomationFramework.Helperes
{
    public static class PathHelper
    {
        private static string GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string GetFullPath(params string[] pathItems)
        {
            var pathWinAssemply = new List<string> { GetAssemblyPath() };
            pathWinAssemply.AddRange(pathItems);

            return Path.Combine(pathWinAssemply.ToArray());
        }

        public static string GetDownloadsPath() =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
    }
}