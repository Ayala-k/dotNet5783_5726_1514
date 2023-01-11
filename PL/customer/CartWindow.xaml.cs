using BL.BlApi;
using BL.BO;
using DO;
using PL.manager.Orders;
using PL.Products;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.customer;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 Action<ProductItem> action;

 Action<int, int> action2;
 public Cart cart2
 {
  get { return (Cart)GetValue(cart2Property); }
  set { SetValue(cart2Property, value); }
 }
 public static readonly DependencyProperty cart2Property =
     DependencyProperty.Register(nameof(cart2), typeof(Cart), typeof(CartWindow));

 public BL.BO.OrderItem selectedItem
 {
  get { return (BL.BO.OrderItem)GetValue(selectedItemProperty); }
  set { SetValue(selectedItemProperty, value); }
 }
 public static readonly DependencyProperty selectedItemProperty =
     DependencyProperty.Register("selectedItem", typeof(BL.BO.OrderItem), typeof(CartWindow));

 public ObservableCollection<BL.BO.OrderItem?> orderItemsList
 {
  get { return (ObservableCollection<BL.BO.OrderItem?>)GetValue(orderItemsListProperty); }
  set { SetValue(orderItemsListProperty, value); }
 }
 public static readonly DependencyProperty orderItemsListProperty =
  DependencyProperty.Register(nameof(orderItemsList), typeof(ObservableCollection<BL.BO.OrderItem?>), typeof(CartWindow));
 public string errorMessageText2
 {
  get { return (string)GetValue(errorMessageText2Property); }
  set { SetValue(errorMessageText2Property, value); }
 }
 public static readonly DependencyProperty errorMessageText2Property =
     DependencyProperty.Register("errorMessageText2", typeof(string), typeof(CartWindow));
 public CartWindow(Cart c, Action<ProductItem> action, Action<int, int> action2)
 {
  cart2 = c;
  //orderItemsList = new ObservableCollection<BL.BO.OrderItem>(cart2.ItemsList);
  orderItemsList = new ObservableCollection<BL.BO.OrderItem?>(from BL.BO.OrderItem oi in cart2.ItemsList
                                                              select new BL.BO.OrderItem(oi));
  InitializeComponent();
  this.action = action;
  this.action2 = action2;
 }
 //private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
 //{
 //}
 private void removeButton_Click(object sender, RoutedEventArgs e)
 {
  orderItemsList = new ObservableCollection<BL.BO.OrderItem?>(from BL.BO.OrderItem oi in (bl.Cart.UpdateOrderItemAmountInCart(cart2, ((int)(((Button)sender).Tag)), 0)).ItemsList  
                                                              select new BL.BO.OrderItem(oi));
  cart2 = new Cart()
  {
   CustomerName = cart2.CustomerName,
   CustomerAddress = cart2.CustomerAddress,
   CustomerEmail = cart2.CustomerEmail,
   TotalPrice = cart2.TotalPrice,
   ItemsList = cart2.ItemsList
  };
  action2((int)((Button)sender).Tag, 0);

 }
 private void updateAmountButton_Click(object sender, RoutedEventArgs e)
 {
  //cart2.ItemsList = orderItemsList;
  //BL.BO.OrderItem oiToUpdate = cart2.ItemsList.FirstOrDefault(oi => oi.ProductID == (int)(((Button)sender).Tag));
  //if (oiToUpdate != null)
  try
  {
   MessageBox.Show(cart2.ToString());
   int newAmount = orderItemsList.FirstOrDefault(o => o.ProductID == (int)((Button)sender).Tag).Amount;
   //orderItemsList= PL.PLfunctions.Convert((bl.Cart.UpdateOrderItemAmountInCart(cart2, ((int)(((Button)sender).Tag)), newAmount)).ItemsList);
   /*כדי שיעדכן גם את הכמות הסופית בסל*/
   orderItemsList = new ObservableCollection<BL.BO.OrderItem?>(from BL.BO.OrderItem oi in (bl.Cart.UpdateOrderItemAmountInCart(cart2, ((int)(((Button)sender).Tag)), newAmount)).ItemsList                                                           
                                                               select new BL.BO.OrderItem(oi));
   action2((int)((Button)sender).Tag, cart2.ItemsList.FirstOrDefault(o => o.ProductID == (int)((Button)sender).Tag).Amount);
  }
  catch (BL.BO.EntityNotFoundLogicException exp)
  {
   SetValue(PL.PLfunctions.errorMessageTextProperty, exp.Message.ToString());
   orderItemsList = PL.PLfunctions.Convert(cart2.ItemsList);
  }
  catch (BL.BO.NotEnoughInStockException exp)
  {
   errorMessageText2 = exp.Message.ToString() + ",updated to amount in stock";
   orderItemsList = PL.PLfunctions.Convert(cart2.ItemsList);
   action2((int)((Button)sender).Tag, cart2.ItemsList.FirstOrDefault(o => o.ProductID == (int)((Button)sender).Tag).Amount);
  }
  cart2 = new Cart()
  {
   CustomerName = cart2.CustomerName,
   CustomerAddress = cart2.CustomerAddress,
   CustomerEmail = cart2.CustomerEmail,
   TotalPrice = cart2.TotalPrice,
   ItemsList = cart2.ItemsList
  };
 }
 private void CommitOrderButton_Click(object sender, RoutedEventArgs e)
 {
  int orderID = bl.Cart.CommitOrder(cart2);
 }
}