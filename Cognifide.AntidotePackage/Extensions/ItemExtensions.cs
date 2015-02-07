using Sitecore.Data.Items;
using Sitecore.Install.Framework;
using Sitecore.Install.Items;

namespace Cognifide.AntidotePackage.Extensions
{
    public static class ItemExtensions
    {
        public static ItemSource GenerateItemSource(this Item item, IConverter<Item, PackageEntry> converter)
        {
            return new ItemSource
            {
                Database = "master",
                Root = item.Paths.Path,
                Converter = converter,
                SkipVersions = false,
            };
        }
    }
}
