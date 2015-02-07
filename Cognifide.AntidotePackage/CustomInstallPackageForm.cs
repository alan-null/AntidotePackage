using Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Install;
using Sitecore.Install.Framework;
using Sitecore.Pipelines;
using Sitecore.Shell.Applications.Install.Dialogs.InstallPackage;
using Sitecore.Web.UI.Sheer;

namespace Cognifide.AntidotePackage
{
    public class CustomInstallPackageForm : InstallPackageForm
    {
        private IProcessingContext _ctx;

        /// <param name="message">The message.</param>
        [HandleMessage("installer:startInstallation")]
        protected new void StartInstallation(Message message)
        {
            Assert.ArgumentNotNull((object)message, "message");
            string filename = Installer.GetFilename(this.PackageFile.Value);
            if (FileUtil.IsFile(filename))
            {

                var args = new AntidotePackagePipelineArgs()
                    {
                        SourcePackagePath = filename
                    };
                CorePipeline.Run("GenerateAntidotePackage", args);
            }
            else
            {
                Context.ClientPage.ClientResponse.Alert("Package not found");
                this.Active = "Ready";
                this.BackButton.Disabled = true;
            }
        }
    }
}