using System;
using System.Collections.Generic;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 public LoginWindow()
 {
  InitializeComponent();
 }

 void moveToTrackOrder_Click(object sender, RoutedEventArgs e)
 {
  new OrderTrackingWindow().Show();
  this.Close();
 }

 private void moveToCustomer_Click(object sender, RoutedEventArgs e)
 {
  new ProductItemsListWindow().Show();
  this.Close();
 }

 private void goBackButton_Click(object sender, RoutedEventArgs e)
 {
  new MainWindow().Show();
  this.Close();
 }
}
