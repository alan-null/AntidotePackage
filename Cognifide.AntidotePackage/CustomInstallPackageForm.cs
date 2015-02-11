using Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Install;
using Sitecore.Pipelines;
using Sitecore.Shell.Applications.Install.Dialogs.InstallPackage;
using Sitecore.Web.UI.Sheer;
using System;

namespace Cognifide.AntidotePackage
{
    public class CustomInstallPackageForm : InstallPackageForm
    {
        [HandleMessage("installer:startInstallation")]
        protected new void StartInstallation(Message message)
        {
            Assert.ArgumentNotNull((object)message, "message");
            string filename = Installer.GetFilename(this.PackageFile.Value);
            if (FileUtil.IsFile(filename))
            {

                var args = new AntidotePackagePipelineArgs()
                    {
                        SourcePackagePath = filename,
                        AntidotePackageProject = new PackageProject { Name = PackageFile.Value }
                    };
                CorePipeline.Run("GenerateAntidotePackage", args);
                Sitecore.Shell.Framework.Windows.Close();
            }
            else
            {
                Context.ClientPage.ClientResponse.Alert("Package not found");
                this.Active = "Ready";
                this.Cancel();
                this.BackButton.Disabled = true;
                this.EndWizard();
            }
        }
    }
}