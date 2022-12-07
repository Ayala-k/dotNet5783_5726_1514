using BL.BlApi;

using BL.BO;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
 /// <summary>
 /// Interaction logic for OrderListWindow.xaml
 /// </summary>
 public partial class ProductListWindow : Window
 {
  private IBl bl = new BlImplementation.Bl();

  public ProductListWindow()
  {
   InitializeComponent();
   ProductListview.ItemsSource = bl.Product.GetProducts();
   CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));
  }
  //private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
  //{
  // ProductListview.ItemsSource = bl.Product.GetProducts
  //  (p => p.Category == (DO.Categories)CategoriesSelector.SelectedItem);
  //}

  private void CategoriesSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
  {
   ProductListview.ItemsSource = bl.Product.GetProducts
 (p => p.Category == (DO.Categories)CategoriesSelector.SelectedItem);
  }

  private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
   
  }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
   new ProductWindow().Show();

  }  
  
 }
}
