using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model {
   public class ReferenceTemplateModel : INotifyPropertyChanged {
      public ReferenceTemplateModel(string referenceIdentity, bool? hasLocalCopy = null) {
         ReferenceIdentity = referenceIdentity;
         _hasLocalCopy = hasLocalCopy;
      }

      #region Properties

      private bool? _hasLocalCopy;

      public bool? HasLocalCopy {
         get {
            return _hasLocalCopy;
         }
         set {
            if (!Equals(_hasLocalCopy, value)) {
               _hasLocalCopy = value;
               OnPropertyChanged();
            }
         }
      }

      public string ReferenceIdentity { get; private set; }
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
