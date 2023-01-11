using BL.BO;
using BlImplementation;
using Dal;
using DO;
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

namespace PL.manager.Orders
{
 /// <summary>
 /// Interaction logic for UpdateOrderWindow.xaml
 /// </summary>
 public partial class UpdateOrderWindow : Window
 {
  BL.BlApi.IBl? bl = BlApi.Factory.Get();
  public string errorMessageText
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

  public UpdateOrderWindow(int id,string permission, Action<OrderForList> action=null)
  {
   if (permission == "customer")
   {
    buttonPermission = false;
   }
   shipDate = true;
   deliveryDate = true;
   order = bl.Order.GetOrderDetails(id);
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
   errorMessageText = "";
   //update
   try
   {
    order = bl.Order.UpdateOrderShipping(order.ID);
   }
   catch (ProgressAlreadyDoneException exp)
   {
    errorMessageText = exp.Message.ToString();
   }
   action(bl.Order.GetOrderForListDetails(order.ID));
   shipDate = false;
  }

  private void buttonUpdateDelivery_Click(object sender, RoutedEventArgs e)
  {
   errorMessageText = "";
   try
   {
    order = bl.Order.UpdateOrderDelivering(order.ID);
   }
   catch (ProgressAlreadyDoneException exp)
   {
    errorMessageText = exp.Message.ToString();
   }
   action(bl.Order.GetOrderForListDetails(order.ID));
   deliveryDate = false;
  }
 }
}
