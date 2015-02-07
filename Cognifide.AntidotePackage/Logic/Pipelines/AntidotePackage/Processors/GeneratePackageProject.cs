using Sitecore.Install;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class GeneratePackageProject : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            args.AntidotePackageProject = new PackageProject();
        }
    }
}
