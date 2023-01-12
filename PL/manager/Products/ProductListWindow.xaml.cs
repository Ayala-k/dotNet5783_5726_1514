using BL.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace PL;
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

 public BL.BO.Categories? selectedCategory
 {
  get { return (BL.BO.Categories?)GetValue(selectedCategoryProperty); }
  set { SetValue(selectedCategoryProperty, value); }
 }
 public static readonly DependencyProperty selectedCategoryProperty =
     DependencyProperty.Register("selectedCategory", typeof(BL.BO.Categories?), typeof(ProductListWindow));


 public static Array categories { get; set; } = ((Enum.GetValues(typeof(BL.BO.Categories))));
 //static Array categories = (Enum.GetValues(typeof(DO.Categories)));

 //static int size = categories.Length;
 //static Array[] newCategories { get; set; } = new Array[size];
 //string all { get; set; } = "all";

 public ProductListWindow()
 {
  productsForListList = PL.PLfunctions.Convert(bl.Product.GetProducts());
  selectedCategory = null;
  InitializeComponent();
  //ComboBoxItem newItem = new ComboBoxItem();
  //newItem.Content = "all";
  //CategoriesSelector.ItemsSource.Add(newItem);
  //CategoriesSelector.ItemsSource = (IEnumerable<ComboBoxItem>)CategoriesSelector.ItemsSource;
  //ComboBoxItem newItem = new ComboBoxItem();
  //newItem.Content = "all";
  //CategoriesSelector.ItemsSource = CategoriesSelector.ItemsSource.ToList().AddRange(newItem);
  //CategoriesSelector.DataContext = this;

  //CategoriesSelector.ItemsSource.Add(all);

  // for (int i = 0; i < size; i++)
  // {
  //  newCategories[i] = categories[i];
  // }
  // newCategories[newCategories.Length] =
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

  productsForListList = PL.PLfunctions.Convert(bl.Product.GetProducts(p => p.Category == selectedCategory));
 }

 private void updateProductToList(ProductForList? product)
 {
  var item = productsForListList.FirstOrDefault(item => item.ID == product.ID);
  if (item != null)
   productsForListList[productsForListList.IndexOf(item)] = product;
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
  {
   id = selectedItem.ID;
   new ProductWindow(id, updateProductToList).ShowDialog();
  }
 }
 private void addProductToList(ProductForList? product)
 {
  productsForListList.Add(product);
 }
 /// <summary>
 /// when "add product" is clicked- open add window
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
 private void Button_Click(object sender, RoutedEventArgs e)
 {
  new ProductWindow(addProductToList).ShowDialog();
 }
}

 //private void Button_Click_1(object sender, RoutedEventArgs e)
 //{
 // ProductListview.ItemsSource = productsForListList;
 // CategoriesSelectorText = " ";
 //}