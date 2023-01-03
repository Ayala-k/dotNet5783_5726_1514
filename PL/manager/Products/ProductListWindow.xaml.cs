using BL.BO;
using PL.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;
/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{

 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 //public static IEnumerable<ProductForList?> productsForListList { get; set; } = new List<ProductForList?>();
 public static readonly DependencyProperty productsForListListProperty =
    DependencyProperty.Register(name:"productsForListList", propertyType:typeof(IEnumerable<ProductForList?>),ownerType:typeof(ProductListWindow));
 public IEnumerable<ProductForList?> productsForListList
 {
  get { return (IEnumerable<ProductForList?>)GetValue(productsForListListProperty); }
  set { SetValue(productsForListListProperty, value); }
 }



 //public int MyProperty//דוגמא
 //{
 // get { return (int)GetValue(MyPropertyProperty); }
 // set { SetValue(MyPropertyProperty, value); }
 //}

 // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...


 public static System.Array categories { get; set; } = Enum.GetValues(typeof(Categories));


 //public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
 //{
 // return new ObservableCollection<T>(original);
 //}

 public ProductListWindow()
 {
  productsForListList =(bl.Product.GetProducts());
  SetValue(productsForListListProperty, new List<ProductForList?>());
  SetValue(productsForListListProperty, bl.Product.GetProducts());
  InitializeComponent();
 }

 /// <summary>
 /// category selector
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>


 private void CategoriesSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
 {
  if (CategoriesSelector.Text != " ")
   productsForListList = bl.Product.GetProducts
 (p => p.Category == (DO.Categories)CategoriesSelector.SelectedItem);
 }


 /// <summary>
 /// when a product is clicked- upadte it
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>


 private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
  int id = 0;//bindingלשנות ל
  if (ProductListview.SelectedItem is BL.BO.ProductForList)
   id = ((BL.BO.ProductForList)ProductListview.SelectedItem).ID;
  new ProductWindow("update", id).ShowDialog();
 }

 /// <summary>
 /// when "add product" is clicked- open add window
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>

 private void Button_Click(object sender, RoutedEventArgs e)
 {
  new ProductWindow("add").ShowDialog();
 }
 private void Button_Click_1(object sender, RoutedEventArgs e)
 {
  ProductListview.ItemsSource = productsForListList;
  CategoriesSelector.Text = " ";
 }
}
