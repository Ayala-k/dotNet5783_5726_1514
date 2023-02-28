using BL.BO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL;

/// <summary>
/// Interaction logic for OrdersListWindow.xaml
/// </summary>
public partial class OrdersListWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 public ObservableCollection<OrderForList?> orderForListList
 {
  get { return (ObservableCollection<OrderForList?>)GetValue(orderForListListProperty); }
  set { SetValue(orderForListListProperty, value); }
 }
 public static readonly DependencyProperty orderForListListProperty =
  DependencyProperty.Register(nameof(orderForListList), typeof(ObservableCollection<OrderForList?>), typeof(OrdersListWindow));

 public OrdersListWindow()
 {
  orderForListList = PL.PLfunctions.Convert(bl.Order.GetOrders());
  InitializeComponent();

 }

 private void updateOrder(OrderForList? order)
 {
  var item = orderForListList.FirstOrDefault(item => item.ID == order.ID);
  if (item != null)
   orderForListList[orderForListList.IndexOf(item)] = order;
 }

 private void orderListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
  int id = 0;
  if (tmp.SelectedItem is BL.BO.OrderForList)
  {
   id = ((BL.BO.OrderForList)tmp.SelectedItem).ID;
   new UpdateOrderWindow(id,"manager", updateOrder).ShowDialog();
  }
 }

 private void goBackButton_Click(object sender, RoutedEventArgs e)
 {
  new managerMainWindow().Show();
  this.Close();
 }
}
