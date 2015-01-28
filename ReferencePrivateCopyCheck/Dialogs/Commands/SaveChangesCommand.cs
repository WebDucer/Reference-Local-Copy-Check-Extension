using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands {
   public class SaveChangesCommand : GenericCommand<Window> {
      private readonly IEnumerable<VSProject> _projects;

      public SaveChangesCommand(IEnumerable<VSProject> projects) {
         _projects = projects;
      }

      protected override void OnExecute(Window parameter) {
         parameter.IsEnabled = false;

         if (OnCanExecute(parameter)) {
            _projects
               .Where(w => !w.Project.Saved)
               .ToList()
               .ForEach(fe => fe.Project.Save());
         }

         parameter.IsEnabled = true;

         parameter.Dispatcher.BeginInvoke(new Action(parameter.Close));
      }

      protected override bool OnCanExecute(Window parameter) {
         return _projects != null && _projects.Any(a => !a.Project.Saved);
      }
   }
}
