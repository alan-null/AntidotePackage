using System;
using System.Text.RegularExpressions;
using Sitecore.Data;
using Sitecore.Zip;

namespace Cognifide.AntidotePackage.Extensions
{
    public static class ZipEntryExtensions
    {
        public static bool IsItem(this ZipEntry zipEntry)
        {
            return zipEntry.Name.StartsWith("items");
        }

        public static bool IsFile(this ZipEntry zipEntry)
        {
            return zipEntry.Name.StartsWith("files");
        }

        public static ID GetItemId(this ZipEntry zipEntry)
        {
            string stringId = Regex.Match(zipEntry.Name, "{(.)*}").Value;
            return new ID(stringId);
        }

        public static string GetFilePath(this ZipEntry zipEntry)
        {
            return zipEntry.Name.Replace("files/", String.Empty);
        }
    }
}
