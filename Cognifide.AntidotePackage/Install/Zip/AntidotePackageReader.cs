﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Cognifide.AntidotePackage.Extensions;
using Cognifide.AntidotePackage.Models;
using Cognifide.AntidotePackage.Models.Package;
using Sitecore.Install;
using Sitecore.Zip;

namespace Cognifide.AntidotePackage.Install.Zip
{
    public class AntidotePackageReader
    {
        private string filename;

        public string FileName
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        public AntidotePackageReader() { }

        public AntidotePackageReader(string filename)
        {
            this.filename = filename;
        }



        public AntidotePackageDefinition GetSources()
        {
            var itemsIds = new List<AntidoteItemSourceDefinition>();
            var filesInfo = new List<AntidoteFileSourceDefinition>();

            ZipReader zipReader = new ZipReader(filename, Encoding.UTF8);
            string tempFileName = Path.GetTempFileName();
            ZipEntry entry1 = zipReader.GetEntry("package.zip");
            if (entry1 != null)
            {
                using (FileStream fileStream = File.Create(tempFileName))
                    StreamUtil.Copy(entry1.GetStream(), fileStream, 16384);
                zipReader.Dispose();
                zipReader = new ZipReader(tempFileName, Encoding.UTF8);
            }
            try
            {
                foreach (ZipEntry entry2 in zipReader.Entries)
                {
                    if (entry2.IsItem())
                    {
                        itemsIds.Add((AntidoteItemSourceDefinition)entry2);
                    }
                    if (entry2.IsFile())
                    {
                        string filePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath(), entry2.GetFilePath());
                        filesInfo.Add(new AntidoteFileSourceDefinition { FileInfo = new FileInfo(filePath) });
                    }
                }
                return new AntidotePackageDefinition
                {
                        FilesInfo = filesInfo,
                        ItemsId = itemsIds.Distinct()
                    };
            }
            finally
            {
                zipReader.Dispose();
                File.Delete(tempFileName);
            }
        }
    }
}
