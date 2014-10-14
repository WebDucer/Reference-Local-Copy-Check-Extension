using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model {
   public class ProjectTemplateModel : INotifyPropertyChanged {
      public ProjectTemplateModel(string projectIdentity) {
         ProjectIdentity = projectIdentity;
         AssignedReferences = new List<ReferenceTemplateModel>();
      }
      #region Properties

      public string ProjectIdentity { get; private set; }

      public IEnumerable<ReferenceTemplateModel> AssignedReferences { get; set; }

      #endregion

      #region INotifyPropertyChanged

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
         var handler = PropertyChanged;
         if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
      }

      #endregion
   }
}