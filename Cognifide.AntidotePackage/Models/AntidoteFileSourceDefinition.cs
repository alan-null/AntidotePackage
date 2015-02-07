using System.IO;

namespace Cognifide.AntidotePackage.Models
{
    public class AntidoteFileSourceDefinition : IAntidoteSource
    {
        public FileInfo FileInfo { get; set; }
        public SourceStatus SourceStatus { get; set; }

        public AntidoteFileSourceDefinition()
        {
            SourceStatus = SourceStatus.NotSet;
        }
    }
}
