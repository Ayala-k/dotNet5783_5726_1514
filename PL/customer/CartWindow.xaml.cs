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
using System.Windows.Shapes;

namespace PL.customer
{
 /// <summary>
 /// Interaction logic for CartWindow.xaml
 /// </summary>
 public partial class CartWindow : Window
 {

  BL.BlApi.IBl? bl = BlApi.Factory.Get();

  public Cart cart2
  {
   get { return (Cart)GetValue(cart2Property); }
   set { SetValue(cart2Property, value); }
  }
  public static readonly DependencyProperty cart2Property =
      DependencyProperty.Register(nameof(cart2), typeof(Cart), typeof(ProudctItemWindow));

  public CartWindow(Cart c)
  {
   cart2 = c;
   MessageBox.Show(cart2.ToString());
   InitializeComponent();
  }
 }
}
