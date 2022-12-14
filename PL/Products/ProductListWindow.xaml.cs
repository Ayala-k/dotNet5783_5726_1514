using BL.BlApi;
using BL.BO;
using PL.Products;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;
/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
 private IBl bl = new BlImplementation.Bl();
 public ProductListWindow()
 {
  InitializeComponent();
  CategoriesSelector.Text = "--all--";
  
  //CategoriesSelector.SelectedIndex = "--all categories--";
  ProductListview.ItemsSource = bl.Product.GetProducts();
  CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));
 }

 private void CategoriesSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
 {
  if (CategoriesSelector.Text != " ")
   ProductListview.ItemsSource = bl.Product.GetProducts
 (p => p.Category == (DO.Categories)CategoriesSelector.SelectedItem);
 }
 private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
  int id = 0;
  if (ProductListview.SelectedItem is BL.BO.ProductForList)
   id = ((BL.BO.ProductForList)ProductListview.SelectedItem).ID;
  new ProductWindow("update", id).ShowDialog();
 }
 private void Button_Click(object sender, RoutedEventArgs e)
 {
  new ProductWindow("add").ShowDialog();
  //ProductListview.ItemsSource = bl.Product.GetProducts();
 }
 private void Button_Click_1(object sender, RoutedEventArgs e)
 {
  ProductListview.ItemsSource = bl.Product.GetProducts();

  CategoriesSelector.Text = " ";
 }
}
