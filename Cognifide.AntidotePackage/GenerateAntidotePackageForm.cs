using System;
using Cognifide.AntidotePackage.Logic.Pipelines.AntidotePackage;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Install;
using Sitecore.IO;
using Sitecore.Pipelines;
using Sitecore.Shell.Framework;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;

namespace Cognifide.AntidotePackage
{
    public class GenerateAntidotePackageForm : WizardForm
    {
        protected Edit PackageFile;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackButton.Visible = false;
        }

        [HandleMessage("antidote:startInstallation")]
        protected void StartInstallation(Message message)
        {

            Assert.ArgumentNotNull(message, "message");
            string filename = Installer.GetFilename(PackageFile.Value);
            if (FileUtil.IsFile(filename))
            {

                var args = new AntidotePackagePipelineArgs
                {
                    SourcePackagePath = filename,
                    AntidotePackageProject = new PackageProject { Name = PackageFile.Value }
                };
                CorePipeline.Run("GenerateAntidotePackage", args);
                Windows.Close();
            }
            else
            {
                Context.ClientPage.ClientResponse.Alert("Package not found");
                Active = "LoadPackage";
            }
        }

        protected override void ActivePageChanged(string page, string oldPage)
        {
            if (page == "Installing")
            {
                EnableButtons();
                Context.ClientPage.SendMessage(this, "antidote:startInstallation");
            }
            if (page == "LoadPackage")
            {
                DisableButtons();
            }
        }

        private void EnableButtons()
        {
            ChangeButtonsState(true);
        }

        private void DisableButtons()
        {
            ChangeButtonsState(false);
        }

        private void ChangeButtonsState(bool state)
        {
            NextButton.Disabled = state;
            CancelButton.Disabled = state;
        }

        protected override void OnCancel(object sender, EventArgs formEventArgs)
        {
            Windows.Close();
        }

        [HandleMessage("antidote:browse", true)]
        protected void Browse(ClientPipelineArgs args)
        {
            DialogUtils.Browse(args, PackageFile);
        }

        [HandleMessage("antidote:upload", true)]
        protected void Upload(ClientPipelineArgs args)
        {
            DialogUtils.Upload(args, PackageFile);
        }
    }
}