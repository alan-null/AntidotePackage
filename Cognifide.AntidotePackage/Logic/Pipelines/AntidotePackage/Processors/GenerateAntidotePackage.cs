using System;
using System.IO;
using System.Web.Hosting;
using Sitecore;
using Sitecore.Install;
using Sitecore.Install.Framework;
using Sitecore.Install.Zip;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class GenerateAntidotePackage : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            string antidodeFolderPath = Sitecore.Configuration.Settings.PackagePath + "\\Antidote packages";
            CheckAntidoteFolderPath(antidodeFolderPath);
            string packagePath = Path.Combine(antidodeFolderPath, String.Format("Antidote for {0}", args.AntidotePackageProject.Name)); ;
            ISink<PackageEntry> writer = new PackageWriter(packagePath);
            ITaskOutput output = new DefaultOutput();
            writer.Initialize(new SimpleProcessingContext(output));
            PackageGenerator.GeneratePackage(args.AntidotePackageProject, writer);

            Context.ClientPage.ClientResponse.Alert("Antidote package generated");
        }

        private void CheckAntidoteFolderPath(string antidodeFolderPath)
        {
            if (!Directory.Exists(antidodeFolderPath))
            {
                Directory.CreateDirectory(antidodeFolderPath);
            }
        }
    }
}
