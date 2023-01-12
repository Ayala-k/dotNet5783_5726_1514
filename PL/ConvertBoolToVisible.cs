using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL;

internal class ConvertBoolToVisible: IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
   //value: הערך שאיתו הגענו לפונקציה, כאן בוליאני
   //יחזור הערך לאחר המרה
   if ((bool)value == true)
    return Visibility.Visible;
   return Visibility.Hidden;
  }

  //המרה מיעד למקור
  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
   throw new NotImplementedException();
  }
 }


