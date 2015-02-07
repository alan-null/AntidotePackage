using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;
using Cognifide.AntidotePackage.Extensions;
using Cognifide.AntidotePackage.Models;
using Cognifide.AntidotePackage.Models.Package;
using Newtonsoft.Json;
using Sitecore.Data.Items;
using Sitecore.Install.Framework;

namespace Cognifide.AntidotePackage.Utils
{
    public class AntidotePackageInstallationPostStep : IPostStep
    {
        public void Run()
        {
            Run(null, null);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public void Run(ITaskOutput output, NameValueCollection metaData)
        {
            AttributesContainer attributeses = new AttributesContainer(metaData["Attributes"]);
            object serializedSources = attributeses.GetAttribute("nonexistingsources");
            AntidotePackageDefinition antidotePackageDefinition = JsonConvert.DeserializeObject<AntidotePackageDefinition>(serializedSources as string);

            RemoveSources(antidotePackageDefinition);
        }

        private void RemoveSources(AntidotePackageDefinition antidotePackageDefinition)
        {
            RemoveFiles(antidotePackageDefinition.FilesInfo);
            RemoveItems(antidotePackageDefinition.ItemsId);
        }

        private void RemoveItems(IEnumerable<AntidoteItemSourceDefinition> itemsId)
        {
            foreach (var definition in itemsId)
            {
                Item item = definition.ItemId.GetItem();
                item.Delete();
            }
        }

        private void RemoveFiles(IEnumerable<AntidoteFileSourceDefinition> filesInfo)
        {
            foreach (var definition in filesInfo)
            {
                if (definition.FileInfo.Exists)
                {
                    definition.FileInfo.Delete();
                }
            }
        }
    }
}
