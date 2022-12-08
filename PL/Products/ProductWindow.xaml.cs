using BL.BlApi;
using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace PL.Products
{
 /// <summary>
 /// Interaction logic for ProductWindow.xaml
 /// </summary>
 public partial class ProductWindow : Window
 {
  private IBl bl = new BlImplementation.Bl();

  public ProductWindow()
  {
   InitializeComponent();
   CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));
  }
  public ProductWindow(string str)
  {
   InitializeComponent();
   if (str == "add")
    buttonAddUpdate.Content = "add";
   CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));

  }
  public ProductWindow(string str, int productId)
  {
   Product product = bl.Product.GetProductDetailsManager(productId);
   InitializeComponent();
   if (str == "update")
    buttonAddUpdate.Content = "update";
   CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));
   id.Text = Convert.ToString(productId);
   id.IsReadOnly = true;
   name.Text = product.Name;
   price.Text = Convert.ToString(product.Price);
   //CategoriesSelector.Text = product.Category;
   CategoriesSelector.Text = product.Category.ToString();
   inStock.Text = Convert.ToString(product.InStock);

  }

  private void inStock_TextChanged(object sender, TextChangedEventArgs e)
  {

  }

  private void buttonAddUpdate_Click(object sender, RoutedEventArgs e)
  {
   Product product = new Product()
   {
    ID = Convert.ToInt32(id.Text),//??
    Name = name.Text,
    Category = (BL.BO.Categories)CategoriesSelector.SelectedItem,
    Price = Convert.ToDouble(price.Text),
    InStock = Convert.ToInt32(inStock.Text)
   };
   if (buttonAddUpdate.Content == "add")
    bl.Product.AddProduct(product);
   else
    bl.Product.UpdateProduct(product);

  }

  private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {

  }
 }
}
