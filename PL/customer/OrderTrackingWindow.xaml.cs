﻿using BL.BO;
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
 /// Interaction logic for OrderTrackingWindow.xaml
 /// </summary>
 public partial class OrderTrackingWindow : Window
 {
  BL.BlApi.IBl? bl = BlApi.Factory.Get();
  public OrderTrackingWindow()
  {
   InitializeComponent();
  }
 }
}