using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal;

internal class User : ICart
{
 static string dir = @"..\xml\";

 string userPath = @"XMLUser.xml";

 public int Add(Cart oi)//AddOrderItemToCart
 {
  return 0;
 }

 public void AddOrderItemToCart(DO.OrderItem oi)//AddOrderItemToCart
 {
  XElement ProductRoot = XMLTools.LoadListFromXMLElement(userPath);


  XElement OrderItemElement = new XElement("OrderItem",
              new XElement("ID", oi.ID),
              new XElement("ProductID", oi.ProductID),
              new XElement("OrderID", oi.OrderID),
              new XElement("Price", oi.Price),
              new XElement("Amount", oi.Amount));

  XElement path = XElement.Load(userPath);
  path.Element("itemsList").Add(OrderItemElement);


  //ProductRoot.Add(OrderItemElement);
 }


 public void Delete(int productID)//AddOrderItemFronCart
 {
  //userCart.ItemsList.Remove((userCart.ItemsList.FirstOrDefault(item => item?.ID == productID)));
 }

 public Cart getCart()//to add TotalPrice and ItemsList
 {
  XElement UserRoot = XMLTools.LoadListFromXMLElement(userPath);
  Cart cart = new Cart();

  //cart.CustomerName = UserRoot.Element("Cart").Element("CustomerName").Value;
  //cart.CustomerEmail = UserRoot.Element("Cart").Element("CustomerEmail").Value;
  //cart.CustomerAddress = UserRoot.Element("Cart").Element("CustomerAddress").Value;
  //cart.TotalPrice = Convert.ToInt32(UserRoot.Element("Cart").Element("TotalPrice").Value);
  //cart.ItemsList = (List<DO.OrderItem?>?)(from p in UserRoot.Elements()
  //                                        select p.Element("itemsList")!.Value);
  return cart;
 }


 public IEnumerable<Cart?> GetAll(Func<Cart?, bool>? predict = null)
 {
  throw new NotImplementedException();
 }

 public Cart GetByCondition(Func<Cart?, bool> predict)
 {
  throw new NotImplementedException();
 }

 public void Update(Cart cart)//
 {
  //userCart = cart;
  //Customer name: sdf,
  //      Customer Email: sdf,
  //      Customer Address: sdf,
  //      Total Price: 0,
  //      Items List:
  XElement path = XElement.Load(dir + userPath);
  if(cart.CustomerName!=" ")
  {
   path.Element("Cart").Element("CustomerName").SetValue(cart.CustomerName);
   path.Element("Cart").Element("CustomerEmail").SetValue(cart.CustomerEmail);
   path.Element("Cart").Element("CustomerAddress").SetValue(cart.CustomerAddress);
   path.Save(dir + userPath);

  }




 }

}
//}
