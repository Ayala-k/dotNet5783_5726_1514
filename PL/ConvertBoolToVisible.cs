using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL;

internal class ConvertBoolToVisible: IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
   if ((bool)value == true)
    return Visibility.Visible;
   return Visibility.Hidden;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
   throw new NotImplementedException();
  }
 }


