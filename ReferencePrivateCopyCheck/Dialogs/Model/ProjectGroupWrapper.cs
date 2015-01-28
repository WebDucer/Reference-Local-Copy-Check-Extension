using System.Collections.Generic;
using System.Linq;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels;
using GalaSoft.MvvmLight;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model {
   public class ProjectGroupWrapper : ViewModelBase {
      public string Name { get; set; }

      public string FullName { get; set; }

      public int ReferenceCount {
         get {
            return References.Count();
         }
      }

      public int PrivateCopyCount {
         get {
            return References.Count(c => c.IsPrivateCopy);
         }
      }

      public IEnumerable<ProjectReferenceItemViewModel> References { get; set; }

      public override bool Equals(object obj) {
         var other = obj as ProjectGroupWrapper;
         if (other == null) {
            return false;
         }

         if (!Equals(Name, other.Name)) {
            return false;
         }

         if (!Equals(FullName, other.FullName)) {
            return false;
         }

         return true;
      }

      public override int GetHashCode() {
         return (Name == null ? 0 : Name.GetHashCode()) | (FullName == null ? 0 : FullName.GetHashCode());
      }
   }
}