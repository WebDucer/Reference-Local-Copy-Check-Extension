using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Interfaces;
using GalaSoft.MvvmLight;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class EmptyListViewModel : ViewModelBase, IBasedView {
      #region IBasedView Member

      public bool? OnlyWithLocalCopyOn { get; set; }

      public bool? OnlyExternal { get; set; }

      public bool? OnlyConflicts { get; set; }

      #endregion
   }
}