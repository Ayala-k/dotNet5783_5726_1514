using DO;
using Dal;
using DalApi;
using System.Collections.Generic;
using System.Collections;

namespace DalTest;

public class Program
{
 static IDal IDalVariable = new DalList();

 //private static DalProduct product = new DalProduct();
 //private static DalOrder order = new DalOrder();
 //private static DalOrderItem orderItem = new DalOrderItem();

 public static void Main()
 {
  IDalVariable.Product.initializeDataSource();
  Console.WriteLine("enter 1 to product, 2 to order, 3 to order item, 0 to exit");
  int choice;
  int.TryParse(Console.ReadLine(), out choice);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא

  while (choice != 0)
  {
   switch (choice)
   {
    case 1://product
     {
      Program.productMethod();
      break;
     }
    case 2://order
     {
      Program.orderMethod();
      break;
     }
    case 3://order item
     {
      Program.orderItemMethod();
      break;
     }
   }
   Console.WriteLine("enter 1 to product, 2 to order, 3 to order item, 0 to exit");
   int.TryParse(Console.ReadLine(), out choice);
  }
 }

 /// <summary>
 /// actions on producst
 /// </summary>
 private static void productMethod()
 {
  Product p = new Product();
  Console.WriteLine("enter 1 to add a product, 2 to view a product, 3 to view all products, 4 to delete a product, 5 to update a product ");
  int choiceProduct;
  int.TryParse(Console.ReadLine(), out choiceProduct);
  int parse;

  switch (choiceProduct)
  {
   case 1://add product
    Console.WriteLine("enter product details");
    int.TryParse(Console.ReadLine(), out parse);
    p.ID = parse;
    p.Name = Console.ReadLine();
    int.TryParse(Console.ReadLine(), out parse);
    p.Price = parse;
    int.TryParse(Console.ReadLine(), out parse);
    p.InStock = parse;
    //try
    //{
    IDalVariable.Product.Add(p);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    break;

   case 2://view product
    Console.WriteLine("enter product ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    Console.WriteLine(IDalVariable.Product.GetByCondition(product=>product.ID==parse));
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    break;

   case 3://view all orders
    IEnumerable<Product> pList = IDalVariable.Product.GetAll();
    foreach (Product x in pList)
     Console.WriteLine(x);
    break;

   case 4://delete product
    Console.WriteLine("enter product ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    IDalVariable.Product.Delete(parse);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    break;

   case 5://update product
    Console.WriteLine("enter product ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    p = IDalVariable.Product.GetByCondition(product => product.ID == parse);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    Console.WriteLine(p);
    Console.WriteLine("(enter new product details");
    if (Console.ReadLine() != "")
    {
     p.Name = Console.ReadLine();
     int.TryParse(Console.ReadLine(), out parse);
     p.Price = parse;
     int.TryParse(Console.ReadLine(), out parse);
     p.InStock = parse;
     IDalVariable.Product.Update(p);
    }
    break;
  }
 }

 /// <summary>
 /// actions on orders
 /// </summary>
 private static void orderMethod()
 {
  Order o = new Order();
  Console.WriteLine("enter 1 to add an order, 2 to view an order, 3 to view all orders, 4 to delete an order, 5 to update an order ");
  int choiceOrder;
  int.TryParse(Console.ReadLine(), out choiceOrder);
  DateTime d;
  int parse;

  switch (choiceOrder)
  {
   case 1://add order
    Console.WriteLine("enter order details");
    o.ID = 0;
    o.CustomerName = Console.ReadLine();
    o.CustomerEmail = Console.ReadLine();
    o.CustomerAddress = Console.ReadLine();
    DateTime.TryParse(Console.ReadLine(), out d);
    DateTime.TryParse(Console.ReadLine(), out d);
    o.ShipDate = d;
    DateTime.TryParse(Console.ReadLine(), out d);
    o.DeliveryDate = d;
    IDalVariable.Order.Add(o);
    break;

   case 2://view order
    Console.WriteLine("enter order ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    Console.WriteLine(IDalVariable.Order.GetByCondition(order => order.ID == parse));
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    break;

   case 3://view all orders
    IEnumerable<Order> oList = IDalVariable.Order.GetAll();
    foreach (Order x in oList)
     Console.WriteLine(x);
    break;

   case 4://delete order
    Console.WriteLine("enter order ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    IDalVariable.Order.Delete(parse);
    //}
    //catch (Exception e)
    //{
    //Console.WriteLine(e);
    //}
    break;

   case 5://update order
    Console.WriteLine("enter order ID to update");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    o = IDalVariable.Order.GetByCondition(order => order.ID == parse);
    //}
    //catch (Exception e)
    //{
    //Console.WriteLine(e);
    //}
    Console.WriteLine(o);
    Console.WriteLine("enter new order details");
    if (Console.ReadLine() != "")
    {
     o.CustomerName = Console.ReadLine();
     o.CustomerEmail = Console.ReadLine();
     o.CustomerAddress = Console.ReadLine();
     DateTime.TryParse(Console.ReadLine(), out d);
     o.OrderDate = d;
     DateTime.TryParse(Console.ReadLine(), out d);
     o.ShipDate = d;
     DateTime.TryParse(Console.ReadLine(), out d);
     o.DeliveryDate = d;
     IDalVariable.Order.Update(o);
    }
    break;
  }
 }

 /// <summary>
 /// actions on order items
 /// </summary>
 static void orderItemMethod()
 {
  OrderItem oi = new OrderItem();
  Console.WriteLine("enter 1 to add an order item, 2 to view an order item, 3 to view all order items, 4 to delete an order item, 5 to update an order item, 6 to view product by order ID and product ID, 7 to view products of an order");
  int choiceOrderItem;
  int.TryParse(Console.ReadLine(), out choiceOrderItem);
  int parse;

  switch (choiceOrderItem)
  {
   case 1://add order item
    Console.WriteLine("enter order item details");
    oi.ID = 0;
    int.TryParse(Console.ReadLine(), out parse);
    oi.OrderID = parse;
    int.TryParse(Console.ReadLine(), out parse);
    oi.ProductID = parse;
    int.TryParse(Console.ReadLine(), out parse);
    oi.Amount = parse;
    int.TryParse(Console.ReadLine(), out parse);
    oi.Price = parse;
    IDalVariable.OrderItem.Add(oi);
    break;

   case 2://view order item
    Console.WriteLine("enter order item ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    Console.WriteLine(IDalVariable.OrderItem.GetByCondition(orderItem => orderItem.ID == parse));
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    break;

   case 3://view all order items
    IEnumerable<OrderItem> oiList = IDalVariable.OrderItem.GetAll();
    foreach (OrderItem x in oiList)
     Console.WriteLine(x);
    break;

   case 4://delete order item
    Console.WriteLine("enter order item ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    IDalVariable.OrderItem.Delete(parse);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    break;

   case 5://update order item
    Console.WriteLine("enter order item ID");
    //try
    //{
    int.TryParse(Console.ReadLine(), out parse);
    oi = IDalVariable.OrderItem.GetByCondition(orderItem => orderItem.ID == parse);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e);
    //}
    Console.WriteLine(oi);
    Console.WriteLine("enter order item ID");
    if (Console.ReadLine() != "")
    {
     int.TryParse(Console.ReadLine(), out parse);
     oi.OrderID = parse;
     int.TryParse(Console.ReadLine(), out parse);
     oi.ProductID = parse;
     int.TryParse(Console.ReadLine(), out parse);
     oi.Amount = parse;
     int.TryParse(Console.ReadLine(), out parse);
     oi.Price = parse;
     IDalVariable.OrderItem.Update(oi);
    }
    break;

   case 6://view order item by order and product
    Console.WriteLine("enter order ID and product ID");
    int parse2;
    int.TryParse(Console.ReadLine(), out parse);
    int.TryParse(Console.ReadLine(), out parse2);
    Console.WriteLine(IDalVariable.OrderItem.GetByCondition(item=>item.OrderID==parse&&item.ProductID==parse2));
    break;

   case 7://View an order's items
    Console.WriteLine("enter order ID");
    int ID;
    int.TryParse(Console.ReadLine(), out parse);
    ID = parse;
    IEnumerable<OrderItem> orderItems = IDalVariable.OrderItem.GetAll(item=>item.OrderID==ID);
    foreach (OrderItem x in orderItems)
     Console.WriteLine(x);
    break;
  }
 }
}