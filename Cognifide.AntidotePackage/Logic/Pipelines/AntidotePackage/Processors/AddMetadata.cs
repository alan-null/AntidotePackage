namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class AddMetadata : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            args.AntidotePackageProject.Metadata.PackageName = "Antidote package";
            args.AntidotePackageProject.Metadata.Author = "Cognifide.AntidotePackage";
            args.AntidotePackageProject.Metadata.PostStep = "Cognifide.AntidotePackage.Utils.AntidotePackageInstallationPostStep, Cognifide.AntidotePackage";
        }
    }
}
