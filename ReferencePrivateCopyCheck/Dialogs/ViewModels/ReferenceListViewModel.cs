using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ReferenceListViewModel : INotifyPropertyChanged {
      public ReferenceListViewModel(IEnumerable<VSProject> originProjects, ICollection<ProjectTemplateModel> configurations) {
         var vsProjects = originProjects as VSProject[] ?? originProjects.ToArray();
         ProjectList = vsProjects.Select(s => new ProjectViewModel(s, configurations
            .FirstOrDefault(f => Equals(f.ProjectIdentity, s.Project.UniqueName))
                                                                      ?? new ProjectTemplateModel(s.Project.UniqueName)))
            .ToList();

         ReferenceList = new ListByReferenceViewModel(vsProjects, configurations);

         ApplyReferenceCommand = new ApplyReferenceConfigurationCommand(this);
         SaveChangesCommand = new SaveChangesCommand(vsProjects);
      }

      #region Properties

      public ListByReferenceViewModel ReferenceList { get; set; }

      private bool? _showLocalCopy = null;

      public bool? ShowLocalCopy {
         get {
            return _showLocalCopy;
         }
         set {
            if (!Equals(_showLocalCopy, value)) {
               _showLocalCopy = value;

               if (ProjectList != null) {
                  ProjectList.ToList().ForEach(fe => fe.ShowPrivateCopy = _showLocalCopy);
               }

               OnPropertyChanged();
            }
         }
      }

      private bool _showOnlyConflicts = false;

      public bool ShowOnlyConflicts {
         get {
            return _showOnlyConflicts;
         }
         set {
            if (!Equals(_showOnlyConflicts, value)) {
               _showOnlyConflicts = value;

               if (ProjectList != null) {
                  ProjectList.ToList().ForEach(fe => fe.ShowOnlyConflicts = _showOnlyConflicts);
               }

               OnPropertyChanged();
            }
         }
      }

      private IEnumerable<ProjectViewModel> _projectList;

      public IEnumerable<ProjectViewModel> ProjectList {
         get {
            return _projectList;
         }
         set {
            if (!Equals(_projectList, value)) {
               _projectList = value;
               OnPropertyChanged();
            }
         }
      }

      public ICommand SaveChangesCommand { get; set; }

      public ICommand SaveReferenceCommand { get; set; }

      public ICommand ApplyReferenceCommand { get; private set; }

      public bool HasConflicts {
         get {
            return ProjectList.Any(a => a.ReferenceList.Any(ra => ra.IsLocalCopy != ra.Template.HasLocalCopy));
         }
      }

      #endregion

      public void UpodateReferenceConfiguration(IEnumerable<ProjectTemplateModel> configurations) {
         ProjectList
            .ToList()
            .ForEach(fe => fe.UpdateReferences(configurations
               .FirstOrDefault(f => Equals(f.ProjectIdentity, fe.OriginProject.Project.UniqueName))
                                               ?? new ProjectTemplateModel(fe.OriginProject.Project.UniqueName)));
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