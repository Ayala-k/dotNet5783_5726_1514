// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using DO;/////
using Dal;
//using static Dal.DalProduct;//why not dallist?
//Console.WriteLine("Hello, World!");

namespace DalTest//להוסיף enum,לאתחל מערכים
{
 public class Program
 {
 private static DalProduct product = new DalProduct();
  private static DalOrder order = new DalOrder();
  private static DalOrderItem orderItem = new DalOrderItem();
  
 public static void Main()
  {
   Console.WriteLine("הכנס מספר מ1 עד 3 ו0 ליציאה");
   int choice = Console.Read();
   while (choice!=0)
   {
    switch (choice)
    {
     case 1://מוצר
      {
       productMethod();
       break;
      }
     case 2://הזמנה
      {
       orderMethod();
       break;
      }
     case 3://פריט בהזמנה
      {
       orderItemMethod();
       break;

      }
    }
    Console.WriteLine("הכנס בחירה ");
    choice = Console.Read();
   }



   void productMethod()
   {
    Product p = new Product();
  Console.WriteLine("הכנס 1 להוספת מוצר,2 להצגה,3 להצגת כל המוצרים,4 למחיקה ו5 לעדכון ");
  int choiceProduct = Console.Read();//string choice2 = Console.ReadLine();
    
  switch (choiceProduct)
  {
   case 1://הוספה
    Console.WriteLine("(הכנס פרטי מוצר(עם ירידות שורה בין כל שדה");
    p.ID = Console.Read();
    p.Name = Console.ReadLine();
    p.Price = Console.Read();
    p.InStock = Console.Read();
      try
      {
       product.AddProduct(p);
      }
      catch(Exception e)
      {
       Console.WriteLine(e);
      }
    break;
   case 2://הצגת המוצר
    Console.WriteLine("הכנס מספר מזהה של מוצר");
      try
      {
       Console.WriteLine(product.GetProduct(Console.Read()));
      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      break;
     case 3://הצגת כל המוצרים
      product.GetAllProduct();
     break;
    case 4://מחיקה
     Console.WriteLine("הכנס מספר מזהה של מוצר");
      try
      {
       product.DeleteProduct(Console.Read());

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

     break;
    case 5://עדכון
     Console.WriteLine("הכנס מספר מזהה של מוצר מוצר");
      try
      {
       p = product.GetProduct(Console.Read());

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      Console.WriteLine(p);//המוצר לעדכון הוא
     Console.WriteLine("(הכנס פרטי מוצר חדשים(בלי מספר מזהה");
      if (Console.ReadLine() != "")
      {
       p.Name = Console.ReadLine();
       p.Price = Console.Read();
       p.InStock = Console.Read();
       product.UpdateProduct(p);
      }
      break;
    }
   }



   void orderMethod()
   {
     Order o = new Order();

  Console.WriteLine("הכנס 1 להוספת הזמנה,2 להצגה,3 להצגת כל ההזמנות,4 למחיקה ו5 לעדכון ");
    int choiceOrder = Console.Read();//string choice2 = Console.ReadLine();
    DateTime d;
    switch (choiceOrder)
    {
     case 1://הוספה
      Console.WriteLine("(הכנס פרטי הזמנה(עם ירידות שורה בין כל שדה");
      o.ID = 0;
      o.CustomerName = Console.ReadLine();
      o.CustomerEmail = Console.ReadLine();
      o.CustomerAddress = Console.ReadLine();
      DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
      o.OrderDate = d;
      DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
      o.ShipDate = d;
      DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
      o.DeliveryDate = d;//צריך להמיר גם מספרים?

      order.AddOrder(o);

      break;
     case 2://הצגת ההזמנה
      Console.WriteLine("הכנס מספר מזהה של הזמנה");
      try
      {
       Console.WriteLine(order.GetOrder(Console.Read()));

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      break;
     case 3://הצגת כל ההזמנות
      order.GetAllOrders();
      break;
     case 4://מחיקה
      Console.WriteLine("הכנס מספר מזהה של הזמנה");
      try
      {
       order.DeleteOrder(Console.Read());

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      break;
     case 5://עדכון
      Console.WriteLine("הכנס מספר מזהה של הזמנה לעדכון");
      try
      {
       o = order.GetOrder(Console.Read());

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      Console.WriteLine(o);//המוצר לעדכון הוא
      Console.WriteLine("(הכנס פרטי הזמנה חדשים(בלי מספר מזהה");
      if (Console.ReadLine() != "")
      {
       o.CustomerName = Console.ReadLine();
       o.CustomerEmail = Console.ReadLine();
       o.CustomerAddress = Console.ReadLine();
       DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
       o.OrderDate = d;
       DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
       o.ShipDate = d;
       DateTime.TryParse(Console.ReadLine(), out d);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
       o.DeliveryDate = d;//צריך להמיר גם מספרים?
       order.UpdateOrder(o);
      }
      break;
    }
   }





 void orderItemMethod()
  {
     OrderItem oi = new OrderItem();

  Console.WriteLine("  הכנס 1 להוספת פריט בהזמנה,2 להצגה,3 להצגת כל הפריטים בהזמנה,4 למחיקה ,5 לעדכון, 6 לץץ ו7 לץץץ");
    int choiceOrderItem = Console.Read();//string choice2 = Console.ReadLine();
    switch (choiceOrderItem)
    {
     case 1://הוספה
      Console.WriteLine("(הכנס פרטי פריט בהזמנה(עם ירידות שורה בין כל שדה");
      oi.ID = 0;
      oi.OrderID = Console.Read();
      oi.ProductID = Console.Read();
      oi.Amount = Console.Read();
      oi.Price = Console.Read();
      orderItem.AddOrderItem(oi);
      break;
     case 2://הצגת הפריט
      Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה");
      try
      {
       Console.WriteLine(orderItem.GetOrderItem(Console.Read()));

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      break;
     case 3://הצגת כל הפריטים בהזמנה
      orderItem.GetAllOrderItems();
      break;
     case 4://מחיקה
      Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה");
      try
      {
       orderItem.DeleteOrderItem(Console.Read());

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      break;
     case 5://עדכון
      Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה לעדכון");
      try
      {
       oi = orderItem.GetOrderItem(Console.Read());

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      Console.WriteLine(oi);//המוצר לעדכון הוא
      Console.WriteLine("(הכנס פרטי פריט בהזמנה חדשים(בלי מספר מזהה");
      //p.ID = Console.Read();
      if (Console.ReadLine() != "")
      {
       oi.OrderID = Console.Read();
       oi.ProductID = Console.Read();
       oi.Amount = Console.Read();
       oi.Price = Console.Read();
       orderItem.UpdateOrderItem(oi);
      }
      break;
     case 6://הצגה על פי שתי מזהים
      Console.WriteLine("הכנס מספר מזהה של מוצר ומספר מזהה של הזמנה");
      try
      {
       Console.WriteLine(orderItem.GetOrderItemByOrderAndProduct(Console.Read(), Console.Read()));

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }

      break;

     case 7://הצגת כל הפריטים בהזמנה זו
      Console.WriteLine("הכנס מספר מזהה של הזמנה ");
      try
      {
       Console.WriteLine(orderItem.GetOrderItemsByOrder(Console.Read()));

      }
      catch (Exception e)
      {
       Console.WriteLine(e);
      }


      break;

    }
   }

  }

  
 }
 
 



}




