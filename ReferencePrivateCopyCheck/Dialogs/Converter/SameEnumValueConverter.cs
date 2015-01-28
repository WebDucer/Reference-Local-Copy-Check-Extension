using System;
using System.Globalization;
using System.Windows.Data;

namespace de.webducer.net.extensions.ReferencePrivateCopyCheck.Dialogs.Converter {
   [ValueConversion(typeof (Enum), typeof (bool), ParameterType = typeof (Enum))]
   public class SameEnumValueConverter : IValueConverter {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
         if (value is Enum && parameter is Enum) {
            return Equals(value, parameter);
         }

         return false;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
         if (value is bool && (bool)value && parameter is Enum) {
            return parameter;
         }

         return null;
      }
   }
}