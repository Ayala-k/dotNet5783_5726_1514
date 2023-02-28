using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for managerMainWindow.xaml
/// </summary>
public partial class managerMainWindow : Window
{
 public managerMainWindow()
 {
  InitializeComponent();
 }

 private void moveToProductList_Click(object sender, RoutedEventArgs e)
 {
  new ProductListWindow().Show();
  this.Close();
 }

 private void moveToOrdersList_Click(object sender, RoutedEventArgs e)
 {
  new OrdersListWindow().Show();
  this.Close();
 }
 private void goBackButton_Click(object sender, RoutedEventArgs e)
 {
  new MainWindow().Show();
  this.Close();
 }
}
