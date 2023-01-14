using BL.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace PL;
/// <summary>
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{

    BL.BlApi.IBl? bl = BlApi.Factory.Get();

    public ObservableCollection<ProductForList?> productsForListList
    {
        get { return (ObservableCollection<ProductForList?>)GetValue(productsForListListProperty); }
        set { SetValue(productsForListListProperty, value); }
    }
    public static readonly DependencyProperty productsForListListProperty =
     DependencyProperty.Register(nameof(productsForListList), typeof(ObservableCollection<ProductForList?>), typeof(ProductListWindow));


    public ObservableCollection<ProductForList?> productsForListListToView
    {
        get { return (ObservableCollection<ProductForList?>)GetValue(productsForListListToViewProperty); }
        set { SetValue(productsForListListToViewProperty, value); }
    }
    public static readonly DependencyProperty productsForListListToViewProperty =
     DependencyProperty.Register(nameof(productsForListListToView), typeof(ObservableCollection<ProductForList?>), typeof(ProductListWindow));

    public BL.BO.ProductForList selectedItem
    {
        get { return (BL.BO.ProductForList)GetValue(selectedItemProperty); }
        set { SetValue(selectedItemProperty, value); }
    }
    public static readonly DependencyProperty selectedItemProperty =
        DependencyProperty.Register("selectedItem", typeof(BL.BO.ProductForList), typeof(ProductListWindow));

    public BL.BO.Categories? selectedCategory
    {
        get { return (BL.BO.Categories?)GetValue(selectedCategoryProperty); }
        set { SetValue(selectedCategoryProperty, value); }
    }
    public static readonly DependencyProperty selectedCategoryProperty =
        DependencyProperty.Register("selectedCategory", typeof(BL.BO.Categories?), typeof(ProductListWindow));


    public static Array categories { get; set; } = ((Enum.GetValues(typeof(BL.BO.Categories))));

    public ProductListWindow()
    {
        productsForListList = PL.PLfunctions.Convert(bl.Product.GetProducts());
        productsForListListToView = productsForListList;
        selectedCategory = null;
        InitializeComponent();
    }


    /// <summary>
    /// category selector
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategoriesSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        //productsForListList = PL.PLfunctions.Convert(bl.Product.GetProducts(p => p.Category == selectedCategory));
        var GropupingProducts = (from p in productsForListList
                                 group p by p.Category into categoryGroup
                                 from pr in categoryGroup
                                 where categoryGroup.Key == selectedCategory
                                 select pr).ToList();
        productsForListListToView = PL.PLfunctions.Convert(GropupingProducts);
    }

    private void updateProductToList(ProductForList? product)
    {
        var item = productsForListListToView.FirstOrDefault(item => item.ID == product.ID);
        if (item != null)
            productsForListListToView[productsForListListToView.IndexOf(item)] = product;
    }
    /// <summary>
    /// when a product is clicked- upadte it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int id = 0;
        if (selectedItem is BL.BO.ProductForList)
        {
            id = selectedItem.ID;
            new ProductWindow(id, updateProductToList).ShowDialog();
        }
    }
    private void addProductToList(ProductForList? product)
    {
        productsForListList.Add(product);
        productsForListListToView = productsForListList;
    }
    /// <summary>
    /// when "add product" is clicked- open add window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(addProductToList).ShowDialog();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        productsForListListToView = productsForListList;
    }
}
