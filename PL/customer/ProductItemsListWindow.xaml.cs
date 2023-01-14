using BL.BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace PL;

/// <summary>
/// Interaction logic for ProductItemsListWindow.xaml
/// </summary>

public partial class ProductItemsListWindow : Window
{
    BL.BlApi.IBl? bl = BlApi.Factory.Get();
    public ObservableCollection<ProductItem?> productItemsList
    {
        get { return (ObservableCollection<ProductItem?>)GetValue(productItemsListProperty); }
        set { SetValue(productItemsListProperty, value); }
    }
    public static readonly DependencyProperty productItemsListProperty =
        DependencyProperty.Register(nameof(productItemsList), typeof(ObservableCollection<ProductItem?>), typeof(ProductItemsListWindow));

    public ObservableCollection<ProductItem?> productItemsListToView
    {
        get { return (ObservableCollection<ProductItem?>)GetValue(productItemsListToViewProperty); }
        set { SetValue(productItemsListToViewProperty, value); }
    }
    public static readonly DependencyProperty productItemsListToViewProperty =
        DependencyProperty.Register(nameof(productItemsListToView), typeof(ObservableCollection<ProductItem?>), typeof(ProductItemsListWindow));

    public static Array categories { get; set; } = ((Enum.GetValues(typeof(BL.BO.Categories))));

    public BL.BO.Categories? selectedCategory
    {
        get { return (BL.BO.Categories?)GetValue(selectedCategoryProperty); }
        set { SetValue(selectedCategoryProperty, value); }
    }
    public static readonly DependencyProperty selectedCategoryProperty =
        DependencyProperty.Register("selectedCategory", typeof(BL.BO.Categories?), typeof(ProductItemsListWindow));

    public ProductItem selectedProductItem
    {
        get { return (ProductItem)GetValue(selectedProductItemProperty); }
        set { SetValue(selectedProductItemProperty, value); }
    }
    public static readonly DependencyProperty selectedProductItemProperty =
        DependencyProperty.Register(nameof(selectedProductItem), typeof(ProductItem), typeof(ProductItemsListWindow));

    public Cart cart
    {
        get { return (Cart)GetValue(cartProperty); }
        set { SetValue(cartProperty, value); }
    }
    public static readonly DependencyProperty cartProperty =
        DependencyProperty.Register(nameof(cart), typeof(Cart), typeof(ProductItemsListWindow));
    public ProductItemsListWindow()
    {
        productItemsList = PL.PLfunctions.Convert(bl.Product.GetProductIItems());
        productItemsListToView = productItemsList;
        if (bl.Cart.getUserCart().CustomerName != null)
        {
            cart = bl.Cart.getUserCart();
        }
        else
            cart = new Cart();
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) => new CartWindow(cart, updateProductToList, updateProductToListAmountInCart).ShowDialog();

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var GropupingProducts = (from p in productItemsList
                                 group p by p.Category into categoryGroup
                                 from pr in categoryGroup
                                 where categoryGroup.Key == selectedCategory
                                 select pr).ToList();
        productItemsListToView = PL.PLfunctions.Convert(GropupingProducts);
    }

    private void ButtonGroupingByCategory_Click(object sender, RoutedEventArgs e)
    {
        selectedCategory = null;
        var GropupingProducts = (from p in productItemsList
                                 group p by p.Category into categoryGroup
                                 from pr in categoryGroup
                                 select pr).ToList();

        productItemsListToView = PL.PLfunctions.Convert(GropupingProducts);
    }
    private void updateProductToList(ProductItem productItem)
    {
        var item = productItemsListToView.FirstOrDefault(item => item.ID == productItem.ID);
        if (item != null)
            productItemsListToView[productItemsListToView.IndexOf(item)] = productItem;
    }

    private void updateProductToListAmountInCart(int productID, int amount)
    {
        var item = productItemsListToView.FirstOrDefault(item => item.ID == productID);
        if (item != null)
            item.AmountInCart = amount;
        productItemsListToView[productItemsListToView.IndexOf(item)] = new ProductItem()
        {
            AmountInCart = amount,
            ID = item.ID,
            Name = item.Name,
            Category = item.Category,
            InStock = item.InStock,
            Price = item.Price
        };
    }
    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (selectedProductItem is BL.BO.ProductItem)
            new ProudctItemWindow(selectedProductItem, cart, updateProductToList).Show();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        productItemsListToView = productItemsList;
    }
}
