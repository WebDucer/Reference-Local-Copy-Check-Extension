using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ProjectViewModel : INotifyPropertyChanged {
      public ProjectViewModel(VSProject originProject, ProjectTemplateModel projectConfiguration) {
         if (originProject == null) {
            throw new ArgumentNullException("originProject");
         }

         _originProject = originProject;
         LoadReferences(originProject, projectConfiguration);
      }

      #region Properties

      private readonly VSProject _originProject;

      public VSProject OriginProject {
         get {
            return _originProject;
         }
      }

      public string ProjectName {
         get {
            return _originProject == null ? String.Empty : _originProject.Project.Name;
         }
      }

      private IEnumerable<ReferenceViewModel> _referenceList;

      public IEnumerable<ReferenceViewModel> ReferenceList {
         get {
            return _referenceList;
         }
         private set {
            if (!Equals(_referenceList, value)) {
               _referenceList = value;
               OnPropertyChanged();
            }
         }
      }

      private ICollectionView _filteredReferenceList;

      public ICollectionView FilteredReferenceList {
         get {
            if (_filteredReferenceList == null) {
               _filteredReferenceList = new CollectionViewSource {
                  Source = ReferenceList
               }.View;
               _filteredReferenceList.Filter = (o => {
                  var item = o as ReferenceViewModel;
                  if (item == null) { return false; }

                  if (ShowOnlyConflicts) {
                     return item.IsLocalCopy != item.Template.HasLocalCopy;
                  }

                  return !_showPrivateCopy.HasValue || _showPrivateCopy.Value == item.IsLocalCopy;
               });
            }
            return _filteredReferenceList;
         }
      }

      private bool? _showPrivateCopy;

      public bool? ShowPrivateCopy {
         get {
            return _showPrivateCopy;
         }
         set {
            if (!Equals(_showPrivateCopy, value)) {
               _showPrivateCopy = value;
               if (FilteredReferenceList != null) {
                  FilteredReferenceList.Refresh();
               }
            }
         }
      }

      private bool _showOnlyConflicts;

      public bool ShowOnlyConflicts {
         get {
            return _showOnlyConflicts;
         }
         set {
            if(!Equals(_showOnlyConflicts, value)) {
               _showOnlyConflicts = value;
               if(FilteredReferenceList != null) {
                  FilteredReferenceList.Refresh();
               }
            }
         }
      }

      #endregion

      public void UpdateReferences(ProjectTemplateModel projectConfiguration) {
         ReferenceList
            .ToList()
            .ForEach(fe => fe.Template = projectConfiguration.AssignedReferences
               .FirstOrDefault(f => Equals(f.ReferenceIdentity, fe.ReferenceIdentity))
               ?? new ReferenceTemplateModel(fe.ReferenceIdentity));

         OnPropertyChanged("ShowOnlyConflicts");
      }

      private void LoadReferences(VSProject originProject, ProjectTemplateModel projectConfiguration) {
         if (originProject == null) {
            _referenceList = null;
         }
         else {
            var referenceList = originProject.References
               .OfType<Reference>()
               .Select(reference => new ReferenceViewModel(reference, projectConfiguration.AssignedReferences
                  .FirstOrDefault(f => Equals(reference.Identity, f.ReferenceIdentity))))
               .ToList();

            _referenceList = referenceList;
         }
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