namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Interfaces {
   public interface IBasedView {
      bool? OnlyWithLocalCopyOn { get; set; }

      bool? OnlyExternal { get; set; }

      bool? OnlyConflicts { get; set; }
   }
}