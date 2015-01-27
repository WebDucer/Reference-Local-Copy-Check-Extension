using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ListByReferenceViewModel : INotifyPropertyChanged {
      #region Constructors

      public ListByReferenceViewModel(IEnumerable<VSProject> originProjects, ICollection<ProjectTemplateModel> configurations) {
         var references = originProjects.SelectMany(s => s.References.Cast<Reference>());

         GroupedReferences = references
            .OrderBy(o => o.Name)
            .ThenBy(t => t.ContainingProject.Name)
            .GroupBy(g => new {
               g.Name,
               g.Version
            })
            .Select(s => new RefDiplayWrapper {
               Name = s.Key.Name,
               Version = s.Key.Version,
               References = s.Select(ss => new ProjectReferenceItemViewModel(ss, configurations))
            });
      }

      #endregion

      #region Properties

      #region Grouped References

      private IEnumerable<RefDiplayWrapper> _groupedReferences = null;

      public IEnumerable<RefDiplayWrapper> GroupedReferences {
         get {
            return _groupedReferences;
         }
         set {
            if (!Equals(_groupedReferences, value)) {
               _groupedReferences = value;
               OnPropertyChanged();
            }
         }
      }

      #endregion

      #endregion

      #region Methods

      #endregion

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
         var handler = PropertyChanged;
         if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
      }

      #endregion
   }

   public class RefDiplayWrapper : INotifyPropertyChanged {
      public string Name { get; set; }

      public string Version { get; set; }

      public int ProjectCount {
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
         var other = obj as RefDiplayWrapper;
         if (other == null) {
            return false;
         }

         if (!Equals(Name, other.Name)) {
            return false;
         }

         if (!Equals(Version, other.Version)) {
            return false;
         }

         return true;
      }

      public override int GetHashCode() {
         return (Name == null ? 0 : Name.GetHashCode()) | (Version == null ? 0 : Version.GetHashCode());
      }

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
         var handler = PropertyChanged;
         if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
      }

      #endregion
   }
}