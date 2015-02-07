using System.IO;
using Sitecore.Install.Files;

namespace Cognifide.AntidotePackage.Extensions
{
    public static class FileInfoExtensions
    {
        public static FileSource ToFileSource(this FileInfo fileInfo)
        {
            var fileSource = new FileSource
            {
                Root = fileInfo.FullName,
                Converter = new FileToEntryConverter(),
            };
            return fileSource;
        }
    }
}
