using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels;
using Newtonsoft.Json;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands {
   public class SaveReferenceCommand : GenericCommand<Window> {
      private readonly ReferenceListViewModel _mainModel;
      private readonly string _configFileName;

      public SaveReferenceCommand(ReferenceListViewModel mainModel, string configFileName) {
         _mainModel = mainModel;
         _configFileName = configFileName;
      }

      protected override void OnExecute(Window parameter) {
         if (string.IsNullOrWhiteSpace(_configFileName)) {
            return;
         }

         var referenceConfiguration = _mainModel.ProjectList
            .Select(s => new ProjectTemplateModel(s.OriginProject.Project.UniqueName) {
               AssignedReferences =
                  s.OriginProject.References
                     .OfType<Reference>()
                     .Select(sr => new ReferenceTemplateModel(sr.Identity, sr.CopyLocal))
                     .ToList()
            }).ToList();

      var configurationContent = JsonConvert.SerializeObject(referenceConfiguration, Formatting.Indented);

         File.WriteAllText(_configFileName, configurationContent, Encoding.UTF8);

         _mainModel.UpodateReferenceConfiguration(referenceConfiguration);
      }

      protected override bool OnCanExecute(Window parameter) {
         return _mainModel != null && _mainModel.HasConflicts;
      }
   }
}
