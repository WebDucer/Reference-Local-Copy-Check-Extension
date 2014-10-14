using System;
using System.Linq;
using System.Windows;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands {
   public class ApplyReferenceConfigurationCommand : GenericCommand<Window> {
      private readonly ReferenceListViewModel _mainModel;

      public ApplyReferenceConfigurationCommand(ReferenceListViewModel mainModel) {
         _mainModel = mainModel;
      }

      protected override void OnExecute(Window parameter) {
         if (OnCanExecute(parameter)) {
            // Apply Local Copy Flag from configuration
            _mainModel.ProjectList
               .ToList()
               .ForEach(fe => fe.ReferenceList.ToList().ForEach(rfe => {
                  if (rfe.Template.HasLocalCopy.HasValue && rfe.IsLocalCopy != rfe.Template.HasLocalCopy.Value) {
                     rfe.IsLocalCopy = rfe.Template.HasLocalCopy.Value;
                  }
               }));

            // Save changes
            _mainModel.ProjectList
               .Where(w => !w.OriginProject.Project.Saved)
               .ToList()
               .ForEach(fe => fe.OriginProject.Project.Save());
         }

         parameter.Dispatcher.BeginInvoke(new Action(parameter.Close));
      }

      protected override bool OnCanExecute(Window parameter) {
         return _mainModel != null && _mainModel.HasConflicts;
      }
   }
}