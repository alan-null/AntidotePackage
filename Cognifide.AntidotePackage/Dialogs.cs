using System;
using System.IO;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Globalization;
using Sitecore.IO;
using Sitecore.Shell.Applications.Install;
using Sitecore.Shell.Applications.Install.Dialogs;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Cognifide.AntidotePackage
{
    internal class DialogUtils
    {
        public static void CheckPackageFolder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(ApplicationContext.PackagePath);
            bool flag = FileUtil.FolderExists(directoryInfo.FullName);
            if (directoryInfo.Parent != null && FileUtil.FolderExists(directoryInfo.Parent.FullName) && !flag)
            {
                Directory.CreateDirectory(ApplicationContext.PackagePath);
                Log.Warn(string.Format("The '{0}' folder was not found and has been created. Please check your Sitecore configuration.", ApplicationContext.PackagePath), typeof(DialogUtils));
            }
            if (!Directory.Exists(ApplicationContext.PackagePath))
            {
                throw new ClientAlertException(string.Format(Translate.Text("Cannot access path '{0}'. Please check PackagePath setting in the web.config file."), ApplicationContext.PackagePath));

            }
        }

        public static void Browse(ClientPipelineArgs args, Edit fileEdit)
        {
            try
            {
                CheckPackageFolder();
                if (args.IsPostBack)
                {
                    if (args.HasResult && fileEdit != null)
                    {
                        fileEdit.Value = args.Result;
                    }
                }
                else
                {
                    BrowseDialog.BrowseForOpen(ApplicationContext.PackagePath, "*.zip", "Choose Package", "Click the package that you want to install and then click Open.", "People/16x16/box.png");
                    args.WaitForPostBack();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, typeof(DialogUtils));
                SheerResponse.Alert(ex.Message);
            }
        }

        public static void Upload(ClientPipelineArgs args, Edit fileEdit)
        {
            try
            {
                CheckPackageFolder();
                if (!args.IsPostBack)
                {
                    UploadPackageForm.Show(ApplicationContext.PackagePath, true);
                    args.WaitForPostBack();
                }
                else
                {
                    if (args.Result.StartsWith("ok:", StringComparison.InvariantCulture))
                    {
                        string[] strArray = args.Result.Substring("ok:".Length).Split('|');
                        if (strArray.Length >= 1 && fileEdit != null)
                        {
                            fileEdit.Value = strArray[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, typeof(DialogUtils));
                SheerResponse.Alert(ex.Message);
            }
        }
    }
}
