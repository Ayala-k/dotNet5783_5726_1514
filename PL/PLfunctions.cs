using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PL;

internal class PLfunctions:DependencyObject
{

 //public string errorMessageText
 //{
 // get { return (string)GetValue(errorMessageTextProperty); }
 // set { SetValue(errorMessageTextProperty, value); }
 //}
 //public static readonly DependencyProperty errorMessageTextProperty =
 //    DependencyProperty.Register("errorMessageText", typeof(string), typeof(ProductWindow));
 public static ObservableCollection<T> Convert<T>(IEnumerable<T> original)//convert Ienumerable to ObservableCollection
 {
  return new ObservableCollection<T>(original);
 }

}
