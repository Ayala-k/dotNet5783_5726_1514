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
         product.addProduct(p);
         break;
        case 2://הצגה
         Console.WriteLine("הכנס מספר מזהה של מוצר");
         Console.WriteLine(product.getProduct(Console.Read()));
         break;
        case 3://חסר
         break;
        case 4://מחיקה
         Console.WriteLine("הכנס מספר מזהה של מוצר");
         product.deleteProduct(Console.Read());
         break;
        case 5://עדכון
         Console.WriteLine("הכנס מספר מזהה של מוצר מוצר");
         p = product.getProduct(Console.Read());
         Console.WriteLine(p);//המוצר לעדכון הוא
         Console.WriteLine("(הכנס פרטי מוצר חדשים(בלי מספר מזהה");
         //p.ID = Console.Read();
         if (Console.ReadLine() != "")
         {
          p.Name = Console.ReadLine();
          p.Price = Console.Read();
          p.InStock = Console.Read();
          product.updateProduct(p);
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
         order.addOrder(o);
         break;
        case 2://הצגה
         Console.WriteLine("הכנס מספר מזהה של הזמנה");
         Console.WriteLine(order.getOrder(Console.Read()));
         break;
        case 3://חסר
         break;
        case 4://מחיקה
         Console.WriteLine("הכנס מספר מזהה של הזמנה");
         order.deleteOrder(Console.Read());
         break;
        case 5://עדכון
         Console.WriteLine("הכנס מספר מזהה של הזמנה לעדכון");
         o = order.getOrder(Console.Read());
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
          order.updateOrder(o);
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
         orderItem.addOrderItem(oi);
         break;
        case 2://הצגה
         Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה");
         Console.WriteLine(orderItem.getOrderItem(Console.Read()));
         break;
        case 3://חסר
         break;
        case 4://מחיקה
         Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה");
         orderItem.deleteOrderItem(Console.Read());
         break;
        case 5://עדכון
         Console.WriteLine("הכנס מספר מזהה של פריט בהזמנה לעדכון");
         oi = orderItem.getOrderItem(Console.Read());
         Console.WriteLine(oi);//המוצר לעדכון הוא
         Console.WriteLine("(הכנס פרטי פריט בהזמנה חדשים(בלי מספר מזהה");
         //p.ID = Console.Read();
         if (Console.ReadLine() != "")
         {
          oi.OrderID = Console.Read();
          oi.ProductID = Console.Read();
          oi.Amount = Console.Read();
          oi.Price = Console.Read();
          orderItem.updateOrderItem(oi);
         }
         break;
        case 6://הצגה על פי שתי מזהים
        case 7://הצגת פרטי הזמנה

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



