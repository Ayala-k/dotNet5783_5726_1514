// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using DO;/////
using Dal;
//using static Dal.DalProduct;//why not dallist?
//Console.WriteLine("Hello, World!");

namespace DalTest
{
 public class Program
 {
  private Product p = new Product();
  private Order o = new Order();
  private OrderItem oi = new OrderItem();
  DalProduct product = new DalProduct();
  DalOrder order = new DalOrder();
  DalOrderItem orderItem = new DalOrderItem();


  void main()
  {
   Console.WriteLine("הכנס מספר מ1 עד 3 ו0 ליציאה");//להוסיף enum
   int choice = Console.Read();
   while (choice!=0)
   {
    switch (choice)
    {
     case 1://מוצר
      {
       Console.WriteLine("הכנס בחירה ");
       int choiceProduct = Console.Read();//string choice2 = Console.ReadLine();
       switch (choiceProduct)
       {
        case 1://הוספה
         Console.WriteLine("(הכנס פרטי מוצר(עם ירידות שורה בין כל שדה");
         p.ID = Console.Read();
         p.Name = Console.ReadLine();
         p.Price = Console.Read();
         p.InStock = Console.Read();
         product.AddProduct(p);
         break;
        case 2://הצגה
         Console.WriteLine("הכנס מספר מזהה של מוצר");
         Console.WriteLine(product.GetProduct(Console.Read()));
         break;
        case 3://חסר
         break;
        case 4://מחיקה
         Console.WriteLine("הכנס מספר מזהה של מוצר");
         product.DeleteProduct(Console.Read());
         break;
        case 5://עדכון
         Console.WriteLine("הכנס מספר מזהה של מוצר מוצר");
         p = product.GetProduct(Console.Read());
         Console.WriteLine(p);//המוצר לעדכון הוא
         Console.WriteLine("(הכנס פרטי מוצר חדשים(בלי מספר מזהה");
         //p.ID = Console.Read();
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
      break;
     case 2://הזמנה
      {
       Console.WriteLine("הכנס בחירה ");
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
        case 2://הצגה
         Console.WriteLine("הכנס מספר מזהה של הזמנה");
         Console.WriteLine(order.GetOrder(Console.Read()));
         break;
        case 3://חסר
         break;
        case 4://מחיקה
         Console.WriteLine("הכנס מספר מזהה של הזמנה");
         order.DeleteOrder(Console.Read());
         break;
        case 5://עדכון
         Console.WriteLine("הכנס מספר מזהה של הזמנה לעדכון");
         o = order.GetOrder(Console.Read());
         Console.WriteLine(0);//המוצר לעדכון הוא
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
      break;
     //default:
     // code block
     //break;
     case 3://פריט בהזמנה
      {
       Console.WriteLine("הכנס בחירה ");
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
        case 2://הצגה
         Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה");
         Console.WriteLine(orderItem.GetOrderItem(Console.Read()));
         break;
        case 3://חסר
         break;
        case 4://מחיקה
         Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה");
         orderItem.DeleteOrderItem(Console.Read());
         break;
        case 5://עדכון
         Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה לעדכון");
         oi = orderItem.GetOrderItem(Console.Read());
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
        case 7:
         break;//הצגת פרטי הזמנה

       }
      }
      break;
    }
    Console.WriteLine("הכנס בחירה ");
    choice = Console.Read();
   }
  }
 }
}

//סו,



