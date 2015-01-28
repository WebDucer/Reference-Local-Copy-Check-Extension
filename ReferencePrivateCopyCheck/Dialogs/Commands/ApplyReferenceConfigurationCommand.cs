using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands {
   public class ApplyReferenceConfigurationCommand : GenericCommand<Window> {
      private readonly IEnumerable<VSProject> _projects;
      private readonly IEnumerable<ProjectTemplateModel> _configurations;

      public ApplyReferenceConfigurationCommand(IEnumerable<VSProject> projects, IEnumerable<ProjectTemplateModel> configurations) {
         _projects = projects;
         _configurations = configurations;
      }

      protected override void OnExecute(Window parameter) {
         // Apply Local Copy Flag from configuration
         foreach (var projectConfig in _configurations) {
            var project = _projects.FirstOrDefault(f => projectConfig.ProjectIdentity.Equals(f.Project.UniqueName));

            if (project == null) { continue; }

            foreach (var refConfig in projectConfig.AssignedReferences) {
               var reference = project.References.Cast<Reference>().FirstOrDefault(f => refConfig.ReferenceIdentity.Equals(f.Identity));
               if (reference != null && refConfig.HasLocalCopy.HasValue) {
                  reference.CopyLocal = refConfig.HasLocalCopy.Value;
               }
            }
         }

         // Save changes
         _projects
            .Where(w => !w.Project.Saved)
            .ToList()
            .ForEach(fe => fe.Project.Save());

         parameter.Dispatcher.BeginInvoke(new Action(parameter.Close));
      }
   }
}