using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Model;
using VSLangProj;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.ViewModels {
   public class ReferenceViewModel : INotifyPropertyChanged {
      private readonly Reference _originReference;

      public ReferenceViewModel(Reference originReference, ReferenceTemplateModel configuration) {
         if (originReference == null) {
            throw new ArgumentNullException("originReference");
         }

         _originReference = originReference;
         Template = configuration ?? new ReferenceTemplateModel (_originReference.Identity);
      }

      #region Properties

      public bool IsLocalCopy {
         get {
            return _originReference.CopyLocal;
         }
         set {
            if (!Equals(_originReference.CopyLocal, value)) {
               _originReference.CopyLocal = value;
               OnPropertyChanged();
               OnPropertyChanged("HasConflict");
            }
         }
      }

      public string ReferenceName {
         get {
            return _originReference.Name;
         }
      }

      public string ReferenceIdentity {
         get {
            return _originReference.Identity;
         }
      }

      public Reference OriginReference { get {
         return _originReference;
      } }

      private ReferenceTemplateModel _template;

      public ReferenceTemplateModel Template {
         get {
            return _template;
         }
         internal set {
            if (!Equals(_template, value)) {
               _template = value;
               OnPropertyChanged();
            }
         }
      }

      public bool? HasConflict {
         get {
            return !Template.HasLocalCopy.HasValue
               ? null
               : (bool?)(Template.HasLocalCopy.Value != IsLocalCopy);
         }
      }
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