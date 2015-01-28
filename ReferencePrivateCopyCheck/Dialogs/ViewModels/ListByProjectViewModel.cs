using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Interfaces;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using GalaSoft.MvvmLight;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ListByProjectViewModel : ViewModelBase, IBasedView {
      private readonly IEnumerable<VSProject> _originProjects;
      private readonly ICollection<ProjectTemplateModel> _configurations;

      #region Constructors

      public ListByProjectViewModel(IEnumerable<VSProject> originProjects, ICollection<ProjectTemplateModel> configurations) {
         var vsProjects = originProjects as IList<VSProject> ?? originProjects.ToList();

         _originProjects = vsProjects;
         _configurations = configurations;

         GroupedProjects = vsProjects
            .SelectMany(s => s.References.Cast<Reference>())
            .OrderBy(t => t.ContainingProject.Name)
            .ThenBy(o => o.Name)
            .GroupBy(g => new {
               g.ContainingProject.Name,
               g.ContainingProject.FullName
            })
            .Select(s => new ProjectGroupWrapper {
               Name = s.Key.Name,
               FullName = s.Key.FullName,
               References = s.Select(ss => new ProjectReferenceItemViewModel(ss, configurations))
            });
      }

      #endregion

      #region Properties

      #region Grouped Projects

      private IEnumerable<ProjectGroupWrapper> _groupedProjects = null;

      public IEnumerable<ProjectGroupWrapper> GroupedProjects {
         get {
            return _groupedProjects;
         }
         private set {
            Set(() => GroupedProjects, ref _groupedProjects, value);
         }
      }

      #endregion

      #region Filtered Grouped Reference

      /// <summary>
      ///    The <see cref="FilteredGroupedProjects" /> property's name.
      /// </summary>
      public const string FilteredGroupedReferencesPropertyName = "FilteredGroupedProjects";

      private ICollectionView _filteredGroupedReferences = null;

      /// <summary>
      ///    Sets and gets the FilteredGroupedProjects property.
      ///    Changes to that property's value raise the PropertyChanged event.
      /// </summary>
      public ICollectionView FilteredGroupedProjects {
         get {
            if (_filteredGroupedReferences == null) {
               _filteredGroupedReferences = CollectionViewSource.GetDefaultView(_groupedProjects);
               _filteredGroupedReferences.Filter = FilterReferences;
            }
            return _filteredGroupedReferences;
         }
      }

      #endregion

      #region OnlyExternal

      /// <summary>
      ///    The <see cref="OnlyExternal" /> property's name.
      /// </summary>
      public const string OnlyExternalPropertyName = "OnlyExternal";

      private bool? _onlyExternal = null;

      /// <summary>
      ///    Sets and gets the OnlyExternal property.
      ///    Changes to that property's value raise the PropertyChanged event.
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
      ///    The <see cref="OnlyConflicts" /> property's name.
      /// </summary>
      public const string OnlyConflictsPropertyName = "OnlyConflicts";

      private bool? _onlyConflicts = null;

      /// <summary>
      ///    Sets and gets the OnlyConflicts property.
      ///    Changes to that property's value raise the PropertyChanged event.
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
      ///    The <see cref="OnlyWithLocalCopyOn" /> property's name.
      /// </summary>
      public const string OnlyWithLocalCopyOnPropertyName = "OnlyWithLocalCopyOn";

      private bool? _onlyWithLocalCopyOn = null;

      /// <summary>
      ///    Sets and gets the OnlyWithLocalCopyOn property.
      ///    Changes to that property's value raise the PropertyChanged event.
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

      #region Has Conflicts

      public bool HasConflicts {
         get {
            return GroupedProjects.Any(a => a.References.Any(aa => aa.HasConflict));
         }
      }

      #endregion

      #endregion

      #region Methods

      private bool FilterReferences(object o) {
         var item = o as ProjectGroupWrapper;
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
         if(OnlyWithLocalCopyOn == true && item.References.All(a => !a.IsPrivateCopy)) {
            return false;
         }
         // With no local copy on
         if(OnlyWithLocalCopyOn == false && item.References.All(a => a.IsPrivateCopy)) {
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
}