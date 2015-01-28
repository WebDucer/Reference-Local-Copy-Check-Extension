using GalaSoft.MvvmLight;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model {
   public class ReferenceTemplateModel : ViewModelBase {
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
            Set(() => HasLocalCopy, ref _hasLocalCopy, value);
         }
      }

      public string ReferenceIdentity { get; private set; }

      #endregion
   }
}