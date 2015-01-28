using System;
using EnvDTE;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Design {
   public class DesignReference : Reference {
      private DTE _dte;
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

      public Project ContainingProject { get; set; }

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