using BL.BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

public class Repository:DependencyObject
{
 public static readonly DependencyProperty productsForListListProperty =
DependencyProperty.Register(nameof(productsForListList), typeof(ObservableCollection<ProductForList?>), typeof(ProductListWindow));

 public ObservableCollection<ProductForList?> productsForListList
 {
  get { return (ObservableCollection<ProductForList?>)GetValue(productsForListListProperty); }
  set { SetValue(productsForListListProperty, value); }

 }
 public static ObservableCollection<ProductForList> People { get; set; }
 public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
 {
  return new ObservableCollection<T>(original);
 }
 static Repository()
 {
  People = new ObservableCollection<ProductForList>()
  {
   //new Person("111"){ FirstName="Avraham", LastName="Kohen", Age=9},
   //new Person("222"){ FirstName="Yizchak", LastName="Levi", Age=12},
   //new Person("333"){ FirstName="Yaakov", LastName="Levin", Age=4},
   //new Person("444"){ FirstName="Moshe", LastName="Kahana", Age=21}
  };
 }
}
