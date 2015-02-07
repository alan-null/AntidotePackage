using System.Collections.Generic;
using System.Linq;

namespace Cognifide.AntidotePackage.Models.Package
{
    public class AntidotePackageDefinition
    {
        public IEnumerable<AntidoteItemSourceDefinition> ItemsId = new List<AntidoteItemSourceDefinition>();
        public IEnumerable<AntidoteFileSourceDefinition> FilesInfo = new List<AntidoteFileSourceDefinition>();

        public IEnumerable<AntidoteFileSourceDefinition> GetFileSources(SourceStatus status)
        {
            return FilesInfo.Where(x => x.SourceStatus == status);
        }

        public IEnumerable<AntidoteItemSourceDefinition> GetItemSources(SourceStatus status)
        {
            return ItemsId.Where(x => x.SourceStatus == status);
        }
    }
}
