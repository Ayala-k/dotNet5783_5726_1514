using BL.BO;
using PL.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;
/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{

 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 public ObservableCollection<ProductForList?> productsForListList
 {
  get { return (ObservableCollection<ProductForList?>)GetValue(productsForListListProperty); }
  set { SetValue(productsForListListProperty, value); }
 }
 public static readonly DependencyProperty productsForListListProperty =
  DependencyProperty.Register(nameof(productsForListList), typeof(ObservableCollection<ProductForList?>), typeof(ProductListWindow));

 public BL.BO.ProductForList selectedItem
 {
  get { return (BL.BO.ProductForList)GetValue(selectedItemProperty); }
  set { SetValue(selectedItemProperty, value); }
 }

 public static readonly DependencyProperty selectedItemProperty =
     DependencyProperty.Register("selectedItem", typeof(BL.BO.ProductForList), typeof(ProductListWindow));

 public DO.Categories? selectedCategory
 {
  get { return (DO.Categories?)GetValue(selectedCategoryProperty); }
  set { SetValue(selectedCategoryProperty, value); }
 }

 public static readonly DependencyProperty selectedCategoryProperty =
     DependencyProperty.Register("selectedCategory", typeof(DO.Categories?), typeof(ProductListWindow));

 public static Array categories { get; set; } = (Enum.GetValues(typeof(DO.Categories)));
 
 public static ObservableCollection<T> Convert<T>(IEnumerable<T> original)
 {
  return new ObservableCollection<T>(original);
 }
 public ProductListWindow()
 {
  productsForListList = (bl.Product.GetProducts());
  selectedCategory = null;
  InitializeComponent();
 }

 /// <summary>
 /// category selector
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>

 private void CategoriesSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
 {
  //if (CategoriesSelectorText != " ")
  /*productsForList*/
  productsForListList = Convert(bl.Product.GetProducts(p => p.Category == selectedCategory));
 }

 /// <summary>
 /// when a product is clicked- upadte it
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
 private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
  int id = 0;
  if (selectedItem is BL.BO.ProductForList)
   id = selectedItem.ID;
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
 //private void Button_Click_1(object sender, RoutedEventArgs e)
 //{
 // ProductListview.ItemsSource = productsForListList;
 // CategoriesSelectorText = " ";
 //}
}


