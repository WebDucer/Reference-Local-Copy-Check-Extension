using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Views;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck {
   /// <summary>
   ///    This is the class that implements the package exposed by this assembly.
   ///    The minimum requirement for a class to be considered a valid package for Visual Studio
   ///    is to implement the IVsPackage interface and register itself with the shell.
   ///    This package uses the helper classes defined inside the Managed Package Framework (MPF)
   ///    to do it: it derives from the Package class that provides the implementation of the
   ///    IVsPackage interface and uses the registration attributes defined in the framework to
   ///    register itself and its components with the shell.
   /// </summary>
   // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
   // a package.
   [PackageRegistration(UseManagedResourcesOnly = true)]
   // This attribute is used to register the information needed to show this package
   // in the Help/About dialog of Visual Studio.
   [InstalledProductRegistration("#110", "#112", "0.2", IconResourceID = 400)]
   // This attribute is needed to let the shell know that this package exposes some menus.
   [ProvideMenuResource("Menus.ctmenu", 1)]
   [Guid(GuidList.guidReferencePrivateCopyCheckPkgString)]
   public sealed class ReferencePrivateCopyCheckPackage : Package {
      /// <summary>
      ///    Default constructor of the package.
      ///    Inside this method you can place any initialization code that does not require
      ///    any Visual Studio service because at this point the package object is created but
      ///    not sited yet inside Visual Studio environment. The place to do all the other
      ///    initialization is the Initialize method.
      /// </summary>
      public ReferencePrivateCopyCheckPackage() {
         Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", ToString()));
      }

      /////////////////////////////////////////////////////////////////////////////
      // Overridden Package Implementation

      #region Package Members

      /// <summary>
      ///    Initialization of the package; this method is called right after the package is sited, so this is the place
      ///    where you can put all the initialization code that rely on services provided by VisualStudio.
      /// </summary>
      protected override void Initialize() {
         Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
         base.Initialize();

         // Add our command handlers for menu (commands must exist in the .vsct file)
         var mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
         if (null == mcs) { return; }

         // Create the command for the menu item.
         var menuCommandId = new CommandID(GuidList.guidReferencePrivateCopyCheckCmdSet, (int) PkgCmdIDList.cmdidCheckReferencePrivateCopyFlag);
         var menuItem = new MenuCommand(MenuItemCallback, menuCommandId);
         mcs.AddCommand(menuItem);
      }

      #endregion

      /// <summary>
      ///    This function is the callback used to execute a command when the a menu item is clicked.
      ///    See the Initialize method to see how the menu item is associated to this function using
      ///    the OleMenuCommandService service and the MenuCommand class.
      /// </summary>
      private void MenuItemCallback(object sender, EventArgs e) {
         // Get Solution object
         var vs = GetService(typeof (SDTE)) as DTE2;

         if (vs == null) { return; }

         var solution = vs.Solution;

         if (solution == null) { return; }

         // Load all C# projects
         var vsProjects = solution.Projects.GetCsharpProjects()
            .Where(w => w.Object is VSProject)
            .Select(s => s.Object as VSProject)
            .OrderBy(o => o.Project.Name)
            .ToList();

         // Load reference configuration
         var configurations = new List<ProjectTemplateModel>();
         var configFileName = solution.FileName.GetConfigurationFilePath(Constants.CONFIGURATION_FILE_NAME_EXTENSION);
         if (File.Exists(configFileName)) {
            var configContent = File.ReadAllText(configFileName, Encoding.UTF8);
            configurations = JsonConvert.DeserializeObject<List<ProjectTemplateModel>>(configContent);
         }

         var model = new ReferenceListViewModel(vsProjects, configurations) {
            ShowLocalCopy = null
         };

         var referenceModel = new ListByReferenceViewModel(vsProjects, configurations);
         model.SaveReferenceCommand = new SaveReferenceCommand(model, configFileName);

         var dialog = new ReferenceList {
            DataContext = model
         };

         dialog.ShowDialog();
      }
   }
}