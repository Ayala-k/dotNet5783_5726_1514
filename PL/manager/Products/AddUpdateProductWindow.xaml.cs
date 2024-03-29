﻿using BL.BO;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
 BL.BlApi.IBl? bl = BlApi.Factory.Get();

 private Action<ProductForList> action;
 public static Product product { get; set; } = new Product();
 public static string pageName { get; set; }
 public string errorMessageTextProduct
 {
  get { return (string)GetValue(errorMessageTextProperty); }
  set { SetValue(errorMessageTextProperty, value); }
 }
 public static readonly DependencyProperty errorMessageTextProperty =
     DependencyProperty.Register("errorMessageText", typeof(string), typeof(ProductWindow));

 public static bool isReadOnly { get; set; }

 public static Array categories { get; set; } = Enum.GetValues(typeof(Categories));

 public ProductWindow()
 {
  InitializeComponent();
 }

 /// <summary>
 /// product window ctor for add
 /// </summary>
 /// <param name="str">str=add to make sure the action to be done is adding</param>

 public ProductWindow(Action<ProductForList?> action)
 {
  pageName = "add";
  product = new Product();
  isReadOnly = false;
  errorMessageTextProduct = "";
  InitializeComponent();
  this.action = action;
 }

 /// <summary>
 /// product window ctor for update
 /// </summary>
 /// <param name="str">str=update to make sure the action to be done is updating</param>
 /// <param name="productId">product to be updated</param>

 public ProductWindow(int productId, Action<ProductForList?> action)
 {
  pageName = "update";
  try
  {
   product = bl.Product.GetProductDetailsManager(productId);
  }
  catch (BL.BO.EntityNotFoundLogicException exp)
  {
   errorMessageTextProduct = exp.Message.ToString();
  }
  catch (BL.BO.InvalidDetailsException exp)
  {
   errorMessageTextProduct = exp.Message.ToString();
  }
  isReadOnly = true;
  errorMessageTextProduct = "";
  InitializeComponent();
  this.action = action;
 }

 private void inStock_TextChanged(object sender, TextChangedEventArgs e) { }

 /// <summary>
 /// add or update
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
 private void buttonAddUpdate_Click(object sender, RoutedEventArgs e)
 {
  errorMessageTextProduct = "";
  if (product.ID == 0 || product.Name == "" || product.Category == null
   || product.Price == 0)
  {
   errorMessageTextProduct = "please fill in all fields";
  }
  else
  {
   //add
   if (pageName == "add")
    try
    {
     bl.Product.AddProduct(product);
    }
    catch (InvalidDetailsException exp)
    {
     errorMessageTextProduct = exp.Message.ToString();
    }
    catch (EntityAlreadyExistsLogicException exp)
    {
     errorMessageTextProduct = exp.Message.ToString();
    }
   //update
   else
    try
    {
     bl.Product.UpdateProduct(product);
    }
    catch (InvalidDetailsException exp)
    {
     errorMessageTextProduct = exp.Message.ToString();
    }
    catch (EntityNotFoundLogicException exp)
    {
     errorMessageTextProduct = exp.Message.ToString();
    }
  }
  try
  {
   action(bl!.Product.GetProductForList(product.ID));
  }
  catch (BL.BO.EntityNotFoundLogicException exp)
  {
   errorMessageTextProduct = exp.Message.ToString();
  }
  catch (BL.BO.InvalidDetailsException exp)
  {
   errorMessageTextProduct = exp.Message.ToString();
  }
  //if the action has been done
  if (errorMessageTextProduct == "")
  {
   this.Close();
  }
 }
 
 private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

 /// <summary>
 /// prevent letters in numeric input fields
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
 private void PreviewTextInput(object sender, TextCompositionEventArgs e)
 {
  Regex regex = new Regex("[^0-9]+");
  e.Handled = regex.IsMatch(e.Text);
 }
}
