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

namespace PL.customer
{
 /// <summary>
 /// Interaction logic for CustomerMainWindow.xaml
 /// </summary>
 public partial class CustomerMainWindow : Window
 {
  public CustomerMainWindow()
  {
   InitializeComponent();
  }

  private void Button_Click(object sender, RoutedEventArgs e) => new ProductItemsListWindow().ShowDialog();

  private void Button_Click_1(object sender, RoutedEventArgs e) => new OrderTrackingWindow().ShowDialog();

 }
}
