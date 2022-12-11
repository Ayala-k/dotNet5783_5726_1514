using BL.BlApi;
using BL.BO;
using PL.Products;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL;

/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl = new BlImplementation.Bl();
    public ProductListWindow()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetProducts();
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

    //show all products
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        ProductListview.ItemsSource = bl.Product.GetProducts();
        CategoriesSelector.Text = " ";
    }
}
