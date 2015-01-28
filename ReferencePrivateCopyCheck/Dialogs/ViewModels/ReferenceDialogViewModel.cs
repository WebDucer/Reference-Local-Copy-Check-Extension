using System.Collections.Generic;
using System.Windows.Input;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Interfaces;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using GalaSoft.MvvmLight;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ReferenceDialogViewModel : ViewModelBase {
      private readonly IEnumerable<VSProject> _originProjects;
      private readonly ICollection<ProjectTemplateModel> _configurations;

      private readonly IDictionary<ViewType, IBasedView> _views = new Dictionary<ViewType, IBasedView> {
         {
            ViewType.None, new EmptyListViewModel()
         }
      };

      #region Constructors

      public ReferenceDialogViewModel(IEnumerable<VSProject> originProjects, ICollection<ProjectTemplateModel> configurations) {
         _originProjects = originProjects;
         _configurations = configurations;

         ViewType = ViewType.ReferenceBasedView;

         // Init Commands
         SaveChangesCommand = new SaveChangesCommand(_originProjects);
      }

      #endregion

      #region Properties

      /// <summary>
      ///    The <see cref="ViewType" /> property's name.
      /// </summary>
      public const string ViewTypePropertyName = "ViewType";

      private ViewType _viewType = ViewType.None;

      /// <summary>
      ///    Sets and gets the ViewType property.
      ///    Changes to that property's value raise the PropertyChanged event.
      /// </summary>
      public ViewType ViewType {
         get {
            return _viewType;
         }
         set {
            if (Set(() => ViewType, ref _viewType, value)) {
               if (!_views.ContainsKey(value)) {
                  switch (value) {
                     case ViewType.None:
                        _views.Add(value, new EmptyListViewModel());
                        break;
                     case ViewType.ReferenceBasedView:
                     case ViewType.ProjectBasedView:
                        _views.Add(value, new ListByReferenceViewModel(_originProjects, _configurations));
                        break;
                  }
               }

               CurrentView = _views[value];
            }
         }
      }

      /// <summary>
      ///    The <see cref="CurrentView" /> property's name.
      /// </summary>
      public const string CurrentViewPropertyName = "CurrentView";

      private IBasedView _currentView = null;

      /// <summary>
      ///    Sets and gets the CurrentView property.
      ///    Changes to that property's value raise the PropertyChanged event.
      /// </summary>
      public IBasedView CurrentView {
         get {
            return _currentView;
         }
         private set {
            Set(() => CurrentView, ref _currentView, value);
         }
      }

      public ICommand SaveChangesCommand { get; set; }

      public ICommand ApplyReferenceCommand { get; private set; }

      #endregion
   }
}