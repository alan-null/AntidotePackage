using Cognifide.AntidotePackage.Install.Zip;
using Sitecore.Install;
using Sitecore.Install.Files;
using Sitecore.IO;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class GetAntidotePackageSources : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            string filename = Installer.GetFilename(args.SourcePackagePath);
            if (FileUtil.IsFile(filename))
            {
                string path = PathUtils.MapPath(filename);
                AntidotePackageReader antidotePackageReader = new AntidotePackageReader(path);
                args.AntidotePackageDefinition = antidotePackageReader.GetSources();
            }
        }
    }
}
