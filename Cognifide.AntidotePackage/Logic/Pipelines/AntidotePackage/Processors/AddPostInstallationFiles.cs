using System;
using System.IO;
using System.Reflection;
using Cognifide.AntidotePackage.Extensions;
using Sitecore.Install.Framework;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class AddPostInstallationFiles : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            string p = AssemblyDirectory() + "\\Cognifide.AntidotePackage.dll";
            ISource<PackageEntry> fileSource = new FileInfo(p).ToFileSource();
            args.AntidotePackageProject.Sources.Add(fileSource);
        }

        private string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
