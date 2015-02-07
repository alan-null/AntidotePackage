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
            string packagePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath(), "alan.zip"); ;
            ISink<PackageEntry> writer = new PackageWriter(packagePath);
            ITaskOutput output = new DefaultOutput();
            writer.Initialize(new SimpleProcessingContext(output));
            PackageGenerator.GeneratePackage(args.AntidotePackageProject, writer);

            Context.ClientPage.ClientResponse.Alert("Antidote package generated");
        }
    }
}
