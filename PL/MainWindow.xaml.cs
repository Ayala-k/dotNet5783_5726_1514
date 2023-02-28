using BL.BlApi;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
 private IBl bl = BlApi.Factory.Get();

 public BL.BO.Cart? cart
 {
  get { return (BL.BO.Cart?)GetValue(cartProperty); }
  set { SetValue(cartProperty, value); }
 }
 public static readonly DependencyProperty cartProperty =
     DependencyProperty.Register("cart", typeof(BL.BO.Cart), typeof(MainWindow));

 public MainWindow()
 {
  cart = new BL.BO.Cart()
  {
   CustomerName = " ",
   CustomerEmail = " ",
   CustomerAddress = " ",
   TotalPrice=0
  };
  InitializeComponent();
 }

 void moveToManager_Click(object sender, RoutedEventArgs e)
 {
  new managerMainWindow().Show();
  this.Close();
 }
 private void loginButton_Click(object sender, RoutedEventArgs e)
 {
  bl.Cart.updateUserCart(cart);
  new LoginWindow().Show();
  this.Close();

 }

 private void guestButton_Click(object sender, RoutedEventArgs e)
 {
  bl.Cart.updateUserCart(cart);
  new LoginWindow().Show();
  this.Close();
 }

 private void Button_Click(object sender, RoutedEventArgs e)
 {
  new SimulatorWindow().Show();
 }
}
