using System;
using System.Diagnostics;
using System.Windows.Input;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Commands {
   public abstract class GenericCommand<TParam> : ICommand where TParam : class {
      #region ICommand Members

      [DebuggerStepThrough]
      public virtual bool CanExecute(object parameter) {
         return OnCanExecute(parameter as TParam);
      }

      public event EventHandler CanExecuteChanged {
         add {
            CommandManager.RequerySuggested += value;
         }
         remove {
            CommandManager.RequerySuggested -= value;
         }
      }

      public virtual void Execute(object parameter) {
         OnExecute(parameter as TParam);
      }

      #endregion // ICommand Members

      #region Generic Command Members

      protected abstract void OnExecute(TParam parameter);

      protected virtual bool OnCanExecute(TParam parameter) {
         return true;
      }

      #endregion
   }
}