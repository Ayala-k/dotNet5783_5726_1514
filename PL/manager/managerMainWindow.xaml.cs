﻿using PL.manager.Orders;
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

namespace PL.manager
{
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

  }

  private void moveToOrdersList_Click(object sender, RoutedEventArgs e)
  {
   new OrdersListWindow().Show();
  }
 }
}
