using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels;
using EnvDTE;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Design {
   public class DesignTimeReferenceListViewModel : ReferenceListViewModel {
      public DesignTimeReferenceListViewModel() : base(LoadDesignTimeData(), LoadDesignTimeTemplates()) {
         ShowLocalCopy = false;
      }

      private static IEnumerable<VSProject> LoadDesignTimeData() {
         var proj1Refs = new DesignReferenceList();
         var projRef = proj1Refs.Add("Ref11");
         projRef.CopyLocal = false;
         projRef = proj1Refs.Add("Ref12");
         projRef.CopyLocal = false;
         projRef = proj1Refs.Add("Ref13");
         projRef.CopyLocal = true;
         projRef = proj1Refs.Add("Ref14");
         projRef.CopyLocal = false;

         var proj2Refs = new DesignReferenceList();
         projRef = proj2Refs.Add("Ref21");
         projRef.CopyLocal = false;
         projRef = proj2Refs.Add("Ref22");
         projRef.CopyLocal = false;
         projRef = proj2Refs.Add("Ref23");
         projRef.CopyLocal = true;
         projRef = proj2Refs.Add("Ref24");
         projRef.CopyLocal = false;

         var projectList = new List<VSProject> {
            new DesignVSProject {
               References = proj1Refs,
               Project = new DesignProject {
                  Name = "Project 1",
                  UniqueName = "Project 1",
                  IsDirty = false,
                  Saved = true,
               }
            },
            new DesignVSProject {
               References = proj2Refs,
               Project = new DesignProject {
                  Name = "Project 2",
                  UniqueName = "Project 2",
                  IsDirty = true,
                  Saved = false
               }
            }
         };

         return projectList;
      }

      private static IEnumerable<ProjectTemplateModel> LoadDesignTimeTemplates() {
         return new List<ProjectTemplateModel> {
            new ProjectTemplateModel("Project 1") {
               AssignedReferences = new List<ReferenceTemplateModel> {
                  new ReferenceTemplateModel("Ref11", true),
                  new ReferenceTemplateModel("Ref12", false)
               }
            },
            new ProjectTemplateModel("Project 2") {
               AssignedReferences = new List<ReferenceTemplateModel> {
                  new ReferenceTemplateModel("Ref21", false)
               }
            }
         };
      }

      protected class DesignProject : Project {
         private string _name;
         private string _fileName;
         private bool _isDirty;
         private Projects _collection;
         private DTE _dte;
         private string _kind;
         private ProjectItems _projectItems;
         private Properties _properties;
         private object _o;
         private object _extenderNames;
         private string _extenderCatid;
         private string _fullName;
         private bool _saved;
         private ConfigurationManager _configurationManager;
         private Globals _globals;
         private ProjectItem _parentProjectItem;
         private CodeModel _codeModel;

         public void SaveAs(string NewFileName) {
            throw new NotImplementedException();
         }

         public void Save(string FileName = "") {
            throw new NotImplementedException();
         }

         public void Delete() {
            throw new NotImplementedException();
         }

         public string Name {
            get {
               return _name;
            }
            set {
               _name = value;
            }
         }

         public string FileName {
            get {
               return _fileName;
            }
         }

         public bool IsDirty {
            get {
               return _isDirty;
            }
            set {
               _isDirty = value;
            }
         }

         public Projects Collection {
            get {
               return _collection;
            }
         }

         public DTE DTE {
            get {
               return _dte;
            }
         }

         public string Kind {
            get {
               return _kind;
            }
         }

         public ProjectItems ProjectItems {
            get {
               return _projectItems;
            }
         }

         public Properties Properties {
            get {
               return _properties;
            }
         }

         public string UniqueName { get; set; }

         public object Object {
            get {
               return _o;
            }
         }

         public object get_Extender(string ExtenderName) {
            throw new NotImplementedException();
         }

         public object ExtenderNames {
            get {
               return _extenderNames;
            }
         }

         public string ExtenderCATID {
            get {
               return _extenderCatid;
            }
         }

         public string FullName {
            get {
               return _fullName;
            }
         }

         public bool Saved {
            get {
               return _saved;
            }
            set {
               _saved = value;
            }
         }

         public ConfigurationManager ConfigurationManager {
            get {
               return _configurationManager;
            }
         }

         public Globals Globals {
            get {
               return _globals;
            }
         }

         public ProjectItem ParentProjectItem {
            get {
               return _parentProjectItem;
            }
         }

         public CodeModel CodeModel {
            get {
               return _codeModel;
            }
         }
      }

      protected class DesignVSProject : VSProject {
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

      protected class DesignReferenceList : References {
         private DTE _dte;
         private object _parent;
         private Project _containingProject;
         private int _count;
         private readonly IList<DesignReference> _references = new List<DesignReference>();

         public Reference Item(object index) {
            if (index is int) {
               return _references[(int) index];
            }
            else {
               return null;
            }
         }

         public Reference Find(string bstrIdentity) {
            return _references.FirstOrDefault(f => Equals(bstrIdentity, f.Identity));
         }

         public Reference Add(string bstrPath) {
            var reference = new DesignReference {
               Name = bstrPath,
               Identity = bstrPath
            };
            _references.Add(reference);
            return reference;
         }

         public Reference AddActiveX(string bstrTypeLibGuid, int lMajorVer = 0, int lMinorVer = 0, int lLocaleId = 0, string bstrWrapperTool = "") {
            throw new NotImplementedException();
         }

         public Reference AddProject(Project pProject) {
            throw new NotImplementedException();
         }

         public DTE DTE {
            get {
               return _dte;
            }
         }

         public object Parent {
            get {
               return _parent;
            }
         }

         public Project ContainingProject {
            get {
               return _containingProject;
            }
         }

         public int Count {
            get {
               return _references.Count;
            }
         }

         public IEnumerator GetEnumerator() {
            return _references.GetEnumerator();
         }
      }

      protected class DesignReference : Reference {
         private DTE _dte;
         private Project _containingProject;
         private prjReferenceType _type;
         private string _path;
         private string _description;
         private string _culture;
         private int _majorVersion;
         private int _minorVersion;
         private int _revisionNumber;
         private int _buildNumber;
         private bool _strongName;
         private Project _sourceProject;
         private object _extenderNames;
         private string _extenderCatid;
         private string _publicKeyToken;
         private string _version;

         public void Remove() {
            throw new NotImplementedException();
         }

         public DTE DTE {
            get {
               return _dte;
            }
         }

         public References Collection { get; private set; }

         public Project ContainingProject {
            get {
               return _containingProject;
            }
         }

         public string Name { get; set; }

         public prjReferenceType Type {
            get {
               return _type;
            }
         }

         public string Identity { get; set; }

         public string Path {
            get {
               return _path;
            }
         }

         public string Description {
            get {
               return _description;
            }
         }

         public string Culture {
            get {
               return _culture;
            }
         }

         public int MajorVersion {
            get {
               return _majorVersion;
            }
         }

         public int MinorVersion {
            get {
               return _minorVersion;
            }
         }

         public int RevisionNumber {
            get {
               return _revisionNumber;
            }
         }

         public int BuildNumber {
            get {
               return _buildNumber;
            }
         }

         public bool StrongName {
            get {
               return _strongName;
            }
         }

         public Project SourceProject {
            get {
               return _sourceProject;
            }
         }

         public bool CopyLocal { get; set; }

         public object get_Extender(string ExtenderName) {
            throw new NotImplementedException();
         }

         public object ExtenderNames {
            get {
               return _extenderNames;
            }
         }

         public string ExtenderCATID {
            get {
               return _extenderCatid;
            }
         }

         public string PublicKeyToken {
            get {
               return _publicKeyToken;
            }
         }

         public string Version {
            get {
               return _version;
            }
         }
      }
   }
}