using BL.BO;
using BlImplementation;
using System;
using System.Collections;
using System.Linq;
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
  BL.BlApi.IBl? bl = BlApi.Factory.Get();
  public static Product product { get; set; } = new Product();
  public static string pageName { get; set; }
  public string errorMessageText
  {
   get { return (string)GetValue(errorMessageTextProperty); }
   set { SetValue(errorMessageTextProperty, value); }
  }
  public static readonly DependencyProperty errorMessageTextProperty =
      DependencyProperty.Register("errorMessageText", typeof(string), typeof(ProductWindow));

  public static bool isReadOnly { get; set; }
  public static System.Array categories { get; set; } = Enum.GetValues(typeof(Categories));

  public ProductWindow()
  {
   InitializeComponent();
  }

  /// <summary>
  /// product window ctor for add
  /// </summary>
  /// <param name="str">str=add to make sure the action to be done is adding</param>

  public ProductWindow(string str)
  {
   if (str == "add")
   {
    pageName = "add";
    product = new Product();//אם כפתור העדכון נלחץ קודם 
    isReadOnly = false;
    errorMessageText = "";
   }
   InitializeComponent();
  }

  /// <summary>
  /// product window ctor for update
  /// </summary>
  /// <param name="str">str=update to make sure the action to be done is updating</param>
  /// <param name="productId">product to be updated</param>

  public ProductWindow(string str, int productId)
  {
   if (str == "update")
   {
    pageName = "update";
    product = bl.Product.GetProductDetailsManager(productId);
    isReadOnly = true;
    errorMessageText = "";

   }
   InitializeComponent();
  }

  private void inStock_TextChanged(object sender, TextChangedEventArgs e) { }

  /// <summary>
  /// add or update
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>

  private void buttonAddUpdate_Click(object sender, RoutedEventArgs e)
  {
   errorMessageText = "";
   if (product.ID == 0 || product.Name == "" || product.Category == null
    || product.Price == 0)
   {
    errorMessageText = "please fill in all fields";//לשנות לdependecy property
   }
   else
   {
    //add
    if (pageName == "add")
     try
     {
      bl.Product.AddProduct(product);
     }
     catch (InvalidDetailsException exp)
     {
      errorMessageText = exp.Message.ToString();
     }
     catch (EntityAlreadyExistsLogicException exp)
     {
      errorMessageText = exp.Message.ToString();
     }
    //update
    else
     try
     {
      bl.Product.UpdateProduct(product);
      //SetValue(ProductListWindow.productsForListListProperty, ProductListWindow.Convert(bl.Product.GetProducts()));
     }
     catch (InvalidDetailsException exp)
     {
      errorMessageText = exp.Message.ToString();
     }
     catch (EntityNotFoundLogicException exp)
     {
      errorMessageText = exp.Message.ToString();

     }
   }

   //if the action has been done
   if (errorMessageText == "")
   {
    this.Close();
    foreach (Window w in Application.Current.Windows)
    {
     if (w is ProductListWindow)
     {
      //w.Close();
      ProductListWindow w1= (ProductListWindow)w;
      //לא יכול לעשות השמה לליסט!
      //w1.selectedCategory = null; work

      IEnumerable<BL.BO.ProductForList?> tmp= (bl.Product.GetProducts()).ToList();
      //w1.X1();
      w1.productsForListList = tmp;
      w1.productsForListList = null;//jump to throw
      w1.productsForListList = (bl.Product.GetProducts());
     }
    }
    //new ProductListWindow().Show();
   }
  }
  private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

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
