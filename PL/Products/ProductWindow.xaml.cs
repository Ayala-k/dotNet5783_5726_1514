using BL.BlApi;
using BL.BO;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

  /// <summary>
  /// product window ctor for add
  /// </summary>
  /// <param name="str">str=add to make sure the action to be done is adding</param>

  public ProductWindow(string str)
  {
   InitializeComponent();
   if (str == "add")
   {
    buttonAddUpdate.Content = "add";
    pageName.Content = "add product";
   }
   CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));

  }

  /// <summary>
  /// product window ctor for update
  /// </summary>
  /// <param name="str">str=update to make sure the action to be done is updating</param>
  /// <param name="productId">product to be updated</param>

  public ProductWindow(string str, int productId)
  {
   Product product = bl.Product.GetProductDetailsManager(productId);
   InitializeComponent();
   if (str == "update")
   {
    buttonAddUpdate.Content = "update";
    pageName.Content = "update product";
   }
   CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));
   id.Text = Convert.ToString(productId);
   id.IsReadOnly = true;
   name.Text = product.Name;
   price.Text = Convert.ToString(product.Price);
   //CategoriesSelector.Text = product.Category;
   CategoriesSelector.Text = product.Category.ToString();
   inStock.Text = Convert.ToString(product.InStock);
  }

   private void inStock_TextChanged(object sender, TextChangedEventArgs e) { }

  /// <summary>
  /// add or update
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>

  private void buttonAddUpdate_Click(object sender, RoutedEventArgs e)
  {
   errorMessage.Content = "";
   if (id.Text==""|| name.Text==""|| CategoriesSelector.SelectedItem==null
    || price.Text==""|| inStock.Text=="")
   {
    errorMessage.Content = "please fill in all fields";
   }
   else
   {
    Product product = new Product()
    {
     ID = Convert.ToInt32(id.Text),
     Name = name.Text,
     Category = (BL.BO.Categories)CategoriesSelector.SelectedItem,
     Price = Convert.ToDouble(price.Text),
     InStock = Convert.ToInt32(inStock.Text)
    };

    //add
    if (buttonAddUpdate.Content == "add")
     try
     {
      bl.Product.AddProduct(product);
     }
     catch (InvalidDetailsException exp)
     {
      errorMessage.Content = exp.Message.ToString();
     }
     catch (EntityAlreadyExistsLogicException exp)
     {
      errorMessage.Content = exp.Message.ToString();
     }
    //update
    else
     try
     {
      bl.Product.UpdateProduct(product);
     }
     catch (InvalidDetailsException exp)
     {
      errorMessage.Content = exp.Message.ToString();
     }
     catch (EntityNotFoundLogicException exp)
     {
      errorMessage.Content = exp.Message.ToString();

     }

   }

   //if the action has been done
   if (errorMessage.Content == "")
   {
    this.Close();
    foreach (Window w in Application.Current.Windows)
    {
     if (w is ProductListWindow)
     {
      w.Close();
     }
    }
    new ProductListWindow().Show();
   }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e){}

        /// <summary>
        /// prevent letters in numeric input fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
