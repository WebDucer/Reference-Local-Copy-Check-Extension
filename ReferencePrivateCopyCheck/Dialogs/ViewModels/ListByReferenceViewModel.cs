using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Interfaces;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using GalaSoft.MvvmLight;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ListByReferenceViewModel : ViewModelBase, IBasedView {
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
            Set(() => GroupedReferences, ref _groupedReferences, value);
         }
      }

      #endregion

      #region Filtered Grouped Reference
      /// <summary>
      /// The <see cref="FilteredGroupedReferences" /> property's name.
      /// </summary>
      public const string FilteredGroupedReferencesPropertyName = "FilteredGroupedReferences";

      private ICollectionView _filteredGroupedReferences = null;

      /// <summary>
      /// Sets and gets the FilteredGroupedReferences property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public ICollectionView FilteredGroupedReferences {
         get {
            if (_filteredGroupedReferences == null) {
               _filteredGroupedReferences = CollectionViewSource.GetDefaultView(_groupedReferences);
               _filteredGroupedReferences.Filter = FilterReferences;
            }
            return _filteredGroupedReferences;
         }
      }
      #endregion

      #region OnlyExternal
      /// <summary>
      /// The <see cref="OnlyExternal" /> property's name.
      /// </summary>
      public const string OnlyExternalPropertyName = "OnlyExternal";

      private bool? _onlyExternal = true;

      /// <summary>
      /// Sets and gets the OnlyExternal property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public bool? OnlyExternal {
         get {
            return _onlyExternal;
         }
         set {
            if (Set(() => OnlyExternal, ref _onlyExternal, value)) {
               _filteredGroupedReferences.Refresh();
            }
         }
      }
      #endregion

      #region Only Coonflicts with Configuration
      /// <summary>
      /// The <see cref="OnlyConflicts" /> property's name.
      /// </summary>
      public const string OnlyConflictsPropertyName = "OnlyConflicts";

      private bool? _onlyConflicts = null;

      /// <summary>
      /// Sets and gets the OnlyConflicts property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public bool? OnlyConflicts {
         get {
            return _onlyConflicts;
         }
         set {
            if (Set(() => OnlyConflicts, ref _onlyConflicts, value)) {
               _filteredGroupedReferences.Refresh();
            }
         }
      }
      #endregion

      #region Only with local copy on
      /// <summary>
      /// The <see cref="OnlyWithLocalCopyOn" /> property's name.
      /// </summary>
      public const string OnlyWithLocalCopyOnPropertyName = "OnlyWithLocalCopyOn";

      private bool? _onlyWithLocalCopyOn = null;

      /// <summary>
      /// Sets and gets the OnlyWithLocalCopyOn property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public bool? OnlyWithLocalCopyOn {
         get {
            return _onlyWithLocalCopyOn;
         }
         set {
            if (Set(() => OnlyWithLocalCopyOn, ref _onlyWithLocalCopyOn, value)) {
               _filteredGroupedReferences.Refresh();
            }
         }
      }
      #endregion

      #endregion

      #region Methods

      private bool FilterReferences(object o) {
         var item = o as RefDiplayWrapper;
         if (item == null) {
            return false;
         }
         // External libraries
         if (OnlyExternal == true && item.References.Any(a => a.IsSourceCodeReference)) {
            return false;
         }
         // Not External libraries
         if (OnlyExternal == false && !item.References.Any(a => a.IsSourceCodeReference)) {
            return false;
         }
         // With local copy on
         if (OnlyWithLocalCopyOn == true && item.References.Any(a => !a.IsPrivateCopy)) {
            return false;
         }
         // With no local copy on
         if (OnlyWithLocalCopyOn == false && item.References.All(a => !a.IsPrivateCopy)) {
            return false;
         }
         // With conflicts
         if (OnlyConflicts == true && !item.References.Any(a => a.HasConflict)) {
            return false;
         }
         // With no conflicts
         if (OnlyConflicts == false && item.References.Any(a => a.HasConflict)) {
            return false;
         }

         return true;
      }
      #endregion
   }

   public class RefDiplayWrapper : ViewModelBase {
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
   }
}