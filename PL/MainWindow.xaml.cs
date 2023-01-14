using BL.BlApi;
using BL.BO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL.BlImplementation;
using DO;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
 private IBl bl = BlApi.Factory.Get();

 //BL.BO.Cart? cart = new BL.BO.Cart();



 public BL.BO.Cart? cart
 {
  get { return (BL.BO.Cart?)GetValue(cartProperty); }
  set { SetValue(cartProperty, value); }
 }
 public static readonly DependencyProperty cartProperty =
     DependencyProperty.Register("cart", typeof(BL.BO.Cart), typeof(MainWindow));




 public MainWindow()
 {
  cart = new BL.BO.Cart();
  InitializeComponent();
 }
 void moveToManager_Click(object sender, RoutedEventArgs e) => new managerMainWindow().ShowDialog();

 private void loginButton_Click(object sender, RoutedEventArgs e)
 {
  bl.Cart.updateUserCart(cart);
  new LoginWindow().Show();
 }

 private void guestButton_Click(object sender, RoutedEventArgs e)
 {
  new LoginWindow().Show();
 }
}
