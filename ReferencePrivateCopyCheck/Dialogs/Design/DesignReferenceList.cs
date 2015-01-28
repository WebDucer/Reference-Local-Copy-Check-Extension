using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Design {
   public class DesignReferenceList : References {
      private DTE _dte;
      private object _parent;
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

      public Reference Add(string bstrPath, VSProject project) {
         var reference = new DesignReference {
            Name = bstrPath,
            Identity = bstrPath,
            ContainingProject = project.Project
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

      public Project ContainingProject { get; set; }

      public int Count {
         get {
            return _references.Count;
         }
      }

      public IEnumerator GetEnumerator() {
         return _references.GetEnumerator();
      }

      #region References Member

      public Reference Add(string bstrPath) {
         throw new NotImplementedException();
      }

      #endregion
   }
}