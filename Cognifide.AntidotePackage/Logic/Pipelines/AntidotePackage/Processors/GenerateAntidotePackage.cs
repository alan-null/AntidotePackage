using System;
using System.IO;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Install;
using Sitecore.Install.Framework;
using Sitecore.Install.Zip;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class GenerateAntidotePackage : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            string packagePath = Path.Combine(Settings.PackagePath, String.Format("{0}.antidote{1}", Path.GetFileNameWithoutExtension(args.AntidotePackageProject.Name), Path.GetExtension(args.AntidotePackageProject.Name)));
            ISink<PackageEntry> writer = new PackageWriter(packagePath);
            ITaskOutput output = new DefaultOutput();
            writer.Initialize(new SimpleProcessingContext(output));
            PackageGenerator.GeneratePackage(args.AntidotePackageProject, writer);

            Context.ClientPage.ClientResponse.Alert("Antidote package generated");
        }
    }
}
