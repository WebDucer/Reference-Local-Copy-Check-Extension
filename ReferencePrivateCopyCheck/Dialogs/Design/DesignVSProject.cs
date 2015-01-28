using System;
using EnvDTE;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Design {
   public class DesignVSProject : VSProject {
      private BuildManager _buildManager;
      private DTE _dte;
      private ProjectItem _webReferencesFolder;
      private string _templatePath;
      private bool _workOffline;
      private Imports _imports;
      private VSProjectEvents _events;

      public ProjectItem CreateWebReferencesFolder() {
         throw new NotImplementedException();
      }

      public ProjectItem AddWebReference(string bstrUrl) {
         throw new NotImplementedException();
      }

      public void Refresh() {
         throw new NotImplementedException();
      }

      public void CopyProject(string bstrDestFolder, string bstrDestUNCPath, prjCopyProjectOption copyProjectOption, string bstrUsername,
         string bstrPassword) {
         throw new NotImplementedException();
      }

      public void Exec(prjExecCommand command, int bSuppressUI, object varIn, out object pVarOut) {
         throw new NotImplementedException();
      }

      public void GenerateKeyPairFiles(string strPublicPrivateFile, string strPublicOnlyFile = "0") {
         throw new NotImplementedException();
      }

      public string GetUniqueFilename(object pDispatch, string bstrRoot, string bstrDesiredExt) {
         throw new NotImplementedException();
      }

      public References References { get; set; }

      public BuildManager BuildManager {
         get {
            return _buildManager;
         }
      }

      public DTE DTE {
         get {
            return _dte;
         }
      }

      public Project Project { get; set; }

      public ProjectItem WebReferencesFolder {
         get {
            return _webReferencesFolder;
         }
      }

      public string TemplatePath {
         get {
            return _templatePath;
         }
      }

      public bool WorkOffline {
         get {
            return _workOffline;
         }
         set {
            _workOffline = value;
         }
      }

      public Imports Imports {
         get {
            return _imports;
         }
      }

      public VSProjectEvents Events {
         get {
            return _events;
         }
      }
   }
}