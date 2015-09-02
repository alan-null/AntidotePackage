using Cognifide.AntidotePackage.Models.Package;
using Sitecore.Install;
using Sitecore.Pipelines;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage
{
    public class AntidotePackagePipelineArgs : PipelineArgs
    {
        public string SourcePackagePath { get; set; }
        public AntidotePackageDefinition AntidotePackageDefinition { get; set; }
        public PackageProject AntidotePackageProject { get; set; }
    }
}
