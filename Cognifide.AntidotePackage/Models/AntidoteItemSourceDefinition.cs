using Sitecore.Data;

namespace Cognifide.AntidotePackage.Models
{
    public class AntidoteItemSourceDefinition : IAntidoteSource
    {
        public ID ItemId { get; set; }
        public SourceStatus SourceStatus { get; set; }

        public AntidoteItemSourceDefinition()
        {
            SourceStatus = SourceStatus.NotSet;
        }
    }
}
