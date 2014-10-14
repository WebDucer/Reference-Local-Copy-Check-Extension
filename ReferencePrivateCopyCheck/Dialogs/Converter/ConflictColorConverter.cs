using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Converter {
   [ValueConversion(sourceType: typeof (bool), targetType: typeof (Brush))]
   public class ConflictColorConverter : IValueConverter {
      private static readonly Brush _conflictBrush = new SolidColorBrush(Colors.Red);
      private static readonly Brush _okBrush = new SolidColorBrush(Colors.Transparent);
      private static readonly Brush _undefiniedBrush = new SolidColorBrush(Colors.Yellow);

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
         var conflictValue = value as bool?;
         
         if (conflictValue != null) {
            return conflictValue.Value ? _conflictBrush : _okBrush;
         }
         
         return _undefiniedBrush;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
         throw new NotImplementedException();
      }
   }
}