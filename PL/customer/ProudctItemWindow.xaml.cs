using BL.BO;
using System;
using System.Windows;


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
  if (bl.Cart.getUserCart().CustomerName != "")
  {
   bl.Cart.addOrderItemUserCart(productItem.ID);
   cart = bl.Cart.getUserCart();
   action(productItem);
  }
  else
  {
   try
   {
    bl.Cart.AddOrderItem(cart, productItem.ID);
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
   }
   catch (Exception ex)
   {
    errorMessageText3 = ex.Message;
   }
  }
  if (errorMessageText3 == null)
   this.Close();
 }
}