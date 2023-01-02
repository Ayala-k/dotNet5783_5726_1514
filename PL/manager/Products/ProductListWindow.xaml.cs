using BL.BO;
using PL.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;
/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    
    BL.BlApi.IBl? bl = BlApi.Factory.Get();

    public static ObservableCollection<ProductForList?> productsForListList { get; set; } = new ObservableCollection<ProductForList?>();


    public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
    {
        return new ObservableCollection<T>(original);
    }

    public ProductListWindow()
    {
        InitializeComponent();
        productsForListList = Convert(bl.Product.GetProducts());

        ProductListview.ItemsSource = productsForListList;
        CategoriesSelector.ItemsSource = Enum.GetValues(typeof(Categories));
    }

    /// <summary>
    /// category selector
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    private void CategoriesSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        if (CategoriesSelector.Text != " ")
            ProductListview.ItemsSource = bl.Product.GetProducts
          (p => p.Category == (DO.Categories)CategoriesSelector.SelectedItem);
    }


    /// <summary>
    /// when a product is clicked- upadte it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int id = 0;
        if (ProductListview.SelectedItem is BL.BO.ProductForList)
            id = ((BL.BO.ProductForList)ProductListview.SelectedItem).ID;
        new ProductWindow("update", id).ShowDialog();
    }

    /// <summary>
    /// when "add product" is clicked- open add window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow("add").ShowDialog();
    }
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        ProductListview.ItemsSource = productsForListList;
        CategoriesSelector.Text = " ";
    }
}
