using Sitecore.Data;
using Sitecore.Data.Items;

namespace Cognifide.AntidotePackage.Extensions
{
    public static class IdExtensions
    {
        public static Item GetItem(this ID id)
        {
            return Database.GetDatabase("master").GetItem(id);
        }
    }
}
