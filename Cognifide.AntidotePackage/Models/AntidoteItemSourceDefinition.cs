using System.Text.RegularExpressions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Zip;

namespace Cognifide.AntidotePackage.Models
{
    public class AntidoteItemSourceDefinition : IAntidoteSource
    {
        public ID ItemId { get; set; }
        public SourceStatus SourceStatus { get; set; }
        public string Database { get; set; }

        public AntidoteItemSourceDefinition()
        {
            SourceStatus = SourceStatus.NotSet;
        }

        public static explicit operator AntidoteItemSourceDefinition(ZipEntry zipEntry)
        {
            return new AntidoteItemSourceDefinition
            {
                    ItemId = GetItemId(zipEntry),
                    Database = GetItemDatabase(zipEntry)
                };
        }

        private static ID GetItemId(ZipEntry zipEntry)
        {
            string stringId = Regex.Match(zipEntry.Name, "{(.)*}").Value;
            return new ID(stringId);
        }

        private static string GetItemDatabase(ZipEntry zipEntry)
        {
            return zipEntry.Name.Split('/')[1];
        }

        public Item GetItem()
        {
            return Sitecore.Data.Database.GetDatabase(Database).GetItem(ItemId);
        }
    }
}
