using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for ProudctItemWindow.xaml
/// </summary>
public partial class ProudctItemWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 Action<ProductItem> action;
 public ProductItem productItem
 {
  get { return (ProductItem)GetValue(productItemProperty); }
  set { SetValue(productItemProperty, value); }
 }
 public static readonly DependencyProperty productItemProperty =
     DependencyProperty.Register(nameof(productItem), typeof(ProductItem), typeof(ProudctItemWindow));

 public Cart cart
 {
  get { return (Cart)GetValue(cartProperty); }
  set { SetValue(cartProperty, value); }
 }
 public static readonly DependencyProperty cartProperty =
     DependencyProperty.Register(nameof(cart), typeof(Cart), typeof(ProudctItemWindow));
 public string errorMessageText3
 {
  get { return (string)GetValue(errorMessageText2Property); }
  set { SetValue(errorMessageText2Property, value); }
 }
 public static readonly DependencyProperty errorMessageText2Property =
     DependencyProperty.Register("errorMessageText3", typeof(string), typeof(ProudctItemWindow));

 public ProudctItemWindow(ProductItem pi, Cart c, Action<ProductItem> action)
 {
  cart = c;
  productItem = pi;
  InitializeComponent();
  this.action = action;
  if (productItem.InStock == false)
   errorMessageText3 = "product is out of stock";
 }

 private void AddToCartButton_Click(object sender, RoutedEventArgs e)
 {
  if (bl.Cart.getUserCart().CustomerName != null)//in login
  {
   bl.Cart.addOrderItemUserCart(productItem.ID);
   cart = bl.Cart.getUserCart();
   MessageBox.Show(cart.ItemsList.ToString());
  }
  else
  {
   bl.Cart.AddOrderItem(cart, productItem.ID);
  }//////////
  productItem = new ProductItem()
  {
   ID = productItem.ID,
   Name = productItem.Name,
   Price = productItem.Price,
   AmountInCart = productItem.AmountInCart + 1,
   InStock = productItem.InStock,
   Category = productItem.Category,
  };
  action(productItem);
  this.Close();
 }
}