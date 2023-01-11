using BL.BO;
using PL.Products;
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

namespace PL.customer;

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
  ProductItem? p = new ProductItem();
  productItemsList.Add(p);
  cart = new Cart();
  InitializeComponent();
 }

 private void Button_Click(object sender, RoutedEventArgs e) => new CartWindow(cart, updateProductToList, updateProductToListAmountInCart).ShowDialog();

 private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
  productItemsList = PL.PLfunctions.Convert(bl.Product.GetProductIItems(p => p.Category == selectedCategory));
  //productItemsList = (ObservableCollection<ProductItem?>)(from g in groupByCategory()
  //                                                        where g.Key == selectedCategory
  //                                                        select g);

 }

 private void ButtonGroupingByCategory_Click(object sender, RoutedEventArgs e)
 {
  //productItemsList = (ObservableCollection<ProductItem?>)(from g in groupByCategory()
  //                                                        from a in g
  //                                                        select a);
  //var x = (from pi in productItemsList
  //         group pi by pi.Category into categoryList
  //         from sndjns in categoryList
  //         select sndjns);
  //MessageBox.Show(x.GetType().ToString());
 }
 private void updateProductToList(ProductItem productItem)
 {
  var item = productItemsList.FirstOrDefault(item => item.ID == productItem.ID);
  if (item != null)
   productItemsList[productItemsList.IndexOf(item)] = productItem;
 }

 private void updateProductToListAmountInCart(int productID, int amount)
 {
  var item = productItemsList.FirstOrDefault(item => item.ID == productID);
  if (item != null)
   item.AmountInCart = amount;
  productItemsList[productItemsList.IndexOf(item)] = new ProductItem()
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
 private IEnumerable<IGrouping<Categories?, ProductItem>> groupByCategory()
 {
  IEnumerable<IGrouping<Categories?, ProductItem?>> result = from pi in productItemsList
                                                             group pi by pi.Category into categoryList
                                                             select categoryList;
  return result;
 }
}
