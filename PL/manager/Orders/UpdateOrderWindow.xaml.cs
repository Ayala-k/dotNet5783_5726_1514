using BL.BO;
using System;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for UpdateOrderWindow.xaml
/// </summary>
public partial class UpdateOrderWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();
 public string errorMessageTextOrder
 {
  get { return (string)GetValue(errorMessageTextProperty); }
  set { SetValue(errorMessageTextProperty, value); }
 }
 public static readonly DependencyProperty errorMessageTextProperty =
     DependencyProperty.Register("errorMessageText", typeof(string), typeof(ProductWindow));

 public static bool buttonPermission { get; set; } = true;

 private Action<OrderForList> action;

 public BL.BO.Order order
 {
  get { return (BL.BO.Order)GetValue(orderProperty); }
  set { SetValue(orderProperty, value); }
 }
 public static readonly DependencyProperty orderProperty =
     DependencyProperty.Register(nameof(order), typeof(BL.BO.Order), typeof(UpdateOrderWindow));

 public bool shipDate
 {
  get { return (bool)GetValue(shipDateProperty); }
  set { SetValue(shipDateProperty, value); }
 }
 public static readonly DependencyProperty shipDateProperty =
     DependencyProperty.Register(nameof(shipDate), typeof(bool), typeof(UpdateOrderWindow));

 public bool deliveryDate
 {
  get { return (bool)GetValue(deliveryDateProperty); }
  set { SetValue(deliveryDateProperty, value); }
 }
 public static readonly DependencyProperty deliveryDateProperty =
     DependencyProperty.Register(nameof(deliveryDate), typeof(bool), typeof(UpdateOrderWindow));

 public UpdateOrderWindow(int id, string permission, Action<OrderForList> action = null)
 {
  if (permission == "customer")
  {
   buttonPermission = false;
  }
  shipDate = true;
  deliveryDate = true;
  try
  {
   order = bl.Order.GetOrderDetails(id);
  }
  catch (BL.BO.EntityNotFoundLogicException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  catch (BL.BO.InvalidDetailsException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }

  if (order.DeliveryDate != null)
  {
   shipDate = false;
   deliveryDate = false;
  }
  else if (order.ShipDate != null)
  {
   shipDate = false;
  }
  InitializeComponent();
  this.action = action;
 }

 private void buttonUpdateShipping_Click(object sender, RoutedEventArgs e)
 {
  errorMessageTextOrder = "";
  //update
  try
  {
   order = bl.Order.UpdateOrderShipping(order.ID);
  }
  catch (ProgressAlreadyDoneException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  catch (EntityNotFoundLogicException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  try
  {
   action(bl.Order.GetOrderForListDetails(order.ID));
  }
  catch (BL.BO.EntityNotFoundLogicException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  catch (BL.BO.InvalidDetailsException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  shipDate = false;
 }

 private void buttonUpdateDelivery_Click(object sender, RoutedEventArgs e)
 {
  errorMessageTextOrder = "";
  try
  {
   order = bl.Order.UpdateOrderDelivering(order.ID);
  }
  catch (ProgressAlreadyDoneException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  try
  {
   action(bl.Order.GetOrderForListDetails(order.ID));
  }
  catch (BL.BO.EntityNotFoundLogicException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  catch (BL.BO.InvalidDetailsException exp)
  {
   errorMessageTextOrder = exp.Message.ToString();
  }
  deliveryDate = false;
 }
}
