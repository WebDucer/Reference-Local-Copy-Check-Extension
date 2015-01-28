using System;
using EnvDTE;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Design {
   public class DesignProject : Project {
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
}