using Cognifide.AntidotePackage.Extensions;
using Cognifide.AntidotePackage.Models;
using Sitecore.Data.Items;
using Sitecore.Install.Configuration;
using Sitecore.Install.Framework;
using Sitecore.Install.Items;
using Sitecore.Install.Utils;

namespace Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage.Processors
{
    class AddExistingSources : IAntidotePackagePipelineProcessor
    {
        public void Process(AntidotePackagePipelineArgs args)
        {
            BehaviourOptions options = new BehaviourOptions(InstallMode.Undefined, MergeMode.Undefined);
            IProcessor<PackageEntry> transform = new InstallerConfigurationTransform(options);
            IConverter<Item, PackageEntry> converter = new ItemToEntryConverter();
            converter.Transforms.Add(transform);

            foreach (AntidoteFileSourceDefinition fileSourceDefinition in args.AntidotePackageDefinition.GetFileSources(SourceStatus.Existing))
            {
                ISource<PackageEntry> fileSource = fileSourceDefinition.FileInfo.ToFileSource();
                args.AntidotePackageProject.Sources.Add(fileSource);
            }

            foreach (AntidoteItemSourceDefinition itemSourceDefinition in args.AntidotePackageDefinition.GetItemSources(SourceStatus.Existing))
            {
                Item item = itemSourceDefinition.ItemId.GetItem();
                ItemSource fileSource = item.GenerateItemSource(converter);
                args.AntidotePackageProject.Sources.Add(fileSource);
            }
        }
    }
}
