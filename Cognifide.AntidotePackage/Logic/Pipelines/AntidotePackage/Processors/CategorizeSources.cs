using System.IO;
using Cognifide.AntidotePackage.Models;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class CategorizeSources : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            foreach (var item in args.AntidotePackageDefinition.ItemsId)
            {
                item.SourceStatus = item.GetItem() == null ? SourceStatus.New : SourceStatus.Existing;
            }

            foreach (var fileInfo in args.AntidotePackageDefinition.FilesInfo)
            {
                fileInfo.SourceStatus = File.Exists(fileInfo.FileInfo.FullName) ? SourceStatus.Existing : SourceStatus.New;
            }
        }
    }
}
