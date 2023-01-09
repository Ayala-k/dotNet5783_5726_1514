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

namespace PL.customer;

/// <summary>
/// Interaction logic for ProudctItemWindow.xaml
/// </summary>
public partial class ProudctItemWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();

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
 public ProudctItemWindow(ProductItem pi,Cart c)
 {
  cart = c;
  productItem= pi;
  InitializeComponent();
 }

 private void AddToCartButton_Click(object sender, RoutedEventArgs e)
 {
  bl.Cart.AddOrderItem(cart, productItem.ID);
  MessageBox.Show(cart.ToString());
  productItem.AmountInCart++;
  //upadte list;
 }
}
