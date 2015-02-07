using Cognifide.AntidotePackage.Install;
using Cognifide.AntidotePackage.Models;
using Cognifide.AntidotePackage.Models.Package;
using Newtonsoft.Json;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class AttachNonExistingSourcesInfo : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            AntidotePackageDefinition packageDefinition = new AntidotePackageDefinition
                {
                    FilesInfo = args.AntidotePackageDefinition.GetFileSources(SourceStatus.New),
                    ItemsId = args.AntidotePackageDefinition.GetItemSources(SourceStatus.New)
                };
            string serializedSources = JsonConvert.SerializeObject(packageDefinition);

            AttributesContainer attributeses = new AttributesContainer();
            attributeses.AddAttribute("nonexistingsources", serializedSources);
            args.AntidotePackageProject.Metadata.Attributes = attributeses.ConvertoToPackageAttributes();
        }
    }
}
