using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using Newtonsoft.Json;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands {
   public class SaveReferenceCommand : GenericCommand<Window> {
      private readonly IEnumerable<VSProject> _projects;
      private readonly string _configFileName;

      public SaveReferenceCommand(IEnumerable<VSProject> projects, string configFileName) {
         _projects = projects;
         _configFileName = configFileName;
      }

      protected override void OnExecute(Window parameter) {
         if (string.IsNullOrWhiteSpace(_configFileName)) {
            return;
         }

         var referenceConfiguration = _projects
            .Select(s => new ProjectTemplateModel(s.Project.UniqueName) {
               AssignedReferences =
                  s.References
                     .OfType<Reference>()
                     .Select(sr => new ReferenceTemplateModel(sr.Identity, sr.CopyLocal))
                     .ToList()
            }).ToList();

         var configurationContent = JsonConvert.SerializeObject(referenceConfiguration, Formatting.Indented);

         File.WriteAllText(_configFileName, configurationContent, Encoding.UTF8);
      }
   }
}