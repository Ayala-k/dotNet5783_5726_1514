// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using DO;/////
using System.Xml.Linq;
using Dal;
//using static Dal.DataSource;
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
            product.initializeDataSource();
            Console.WriteLine("enter 1 to product, 2 to order, 3 to order item, 0 to exit");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
            int parse;

            while (choice != 0)
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
                Console.WriteLine("enter 1 to product, 2 to order, 3 to order item, 0 to exit");
                int.TryParse(Console.ReadLine(), out choice);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא

            }



            void productMethod()
            {
                Product p = new Product();
                Console.WriteLine("enter 1 to add a product, 2 to view a product, 3 to view all products, 4 to delete a product, 5 to update a product ");
                int choiceProduct;
                int.TryParse(Console.ReadLine(), out choiceProduct);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא


                switch (choiceProduct)
                {
                    case 1:
                        Console.WriteLine("enter product details");
                        int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        p.ID = parse;
                        p.Name = Console.ReadLine();
                        int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        p.Price = parse;
                        int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                        p.InStock = parse ;
                        try
                        {
                            product.AddProduct(p);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2://הצגת המוצר
                        Console.WriteLine("enter product ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                            Console.WriteLine(product.GetProduct(parse));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                    case 3://הצגת כל המוצרים
                        //Console.WriteLine(product.GetAllProduct()) ;
                        Product[] pArr= product.GetAllProduct();
                        Console.WriteLine("length"+ pArr.Length);
                        for(int i=0;i< pArr.Length; i++)
                        {
                            Console.WriteLine(pArr[i]);
                        }
                        break;
                    case 4://מחיקה
                        Console.WriteLine("enter product ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                            product.DeleteProduct(parse);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                    case 5://עדכון
                        Console.WriteLine("enter product ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא

                            p = product.GetProduct(parse);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        Console.WriteLine(p);//המוצר לעדכון הוא
                        Console.WriteLine("(enter new product details");
                        if (Console.ReadLine() != "")
                        {
                            p.Name = Console.ReadLine();
                            int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                            p.Price = parse;
                            int.TryParse(Console.ReadLine(), out parse);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                            p.InStock = parse;
                            product.UpdateProduct(p);
                        }
                        break;
                }
            }



            void orderMethod()
            {
                Order o = new Order();

                Console.WriteLine("enter 1 to add an order, 2 to view an order, 3 to view all orders, 4 to delete an order, 5 to update an order ");
                int choiceOrder;
                int.TryParse(Console.ReadLine(), out choiceOrder);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                DateTime d;
                switch (choiceOrder)
                {
                    case 1://הוספה
                        Console.WriteLine("enter order details");
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
                        Console.WriteLine("enter order ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            Console.WriteLine(order.GetOrder(parse));
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
                        Console.WriteLine("enter order ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            order.DeleteOrder(parse);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        break;
                    case 5://עדכון
                        Console.WriteLine("enter order ID to update");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            o = order.GetOrder(parse);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        Console.WriteLine(o);//המוצר לעדכון הוא
                        Console.WriteLine("enter new order details");
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
                            o.DeliveryDate = d;
                            order.UpdateOrder(o);
                        }
                        break;
                }
            }

            void orderItemMethod()
            {
                OrderItem oi = new OrderItem();
                Console.WriteLine("enter 1 to add an order item, 2 to view an order item, 3 to view all order items, 4 to delete an order item, 5 to update an order item, 6 to view product by order ID and product ID, 7 to view products of an order");
                int choiceOrderItem;
                int.TryParse(Console.ReadLine(), out choiceOrderItem);//ממיר ומחזיר אמת או שקר אם ההמרה הצליחה או לא
                switch (choiceOrderItem)
                {
                    case 1://הוספה
                        Console.WriteLine("enter order item details");
                        oi.ID = 0;
                        int.TryParse(Console.ReadLine(), out parse);
                        oi.OrderID = parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        oi.ProductID =parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        oi.Amount =parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        oi.Price = parse;
                        orderItem.AddOrderItem(oi);
                        break;

                    case 2://הצגת הפריט
                        Console.WriteLine("enter order item ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            Console.WriteLine(orderItem.GetOrderItem(parse));

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
                        Console.WriteLine("enter order item ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            orderItem.DeleteOrderItem(parse);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        break;

                    case 5://עדכון
                        Console.WriteLine("enter order item ID");
                        try
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            oi = orderItem.GetOrderItem(parse);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        Console.WriteLine(oi);//המוצר לעדכון הוא
                        Console.WriteLine("enter new order item ID");
                        if (Console.ReadLine() != "")
                        {
                            int.TryParse(Console.ReadLine(), out parse);
                            oi.OrderID =parse;
                            int.TryParse(Console.ReadLine(), out parse);
                            oi.ProductID =parse;
                            int.TryParse(Console.ReadLine(), out parse);
                            oi.Amount = parse;
                            int.TryParse(Console.ReadLine(), out parse);
                            oi.Price = parse;
                            orderItem.UpdateOrderItem(oi);
                        }
                        break;

                    case 6://הצגה על פי שתי מזהים
                        Console.WriteLine("enter order ID and product ID");
                        try
                        {
                            int parse2;
                            int.TryParse(Console.ReadLine(), out parse);
                            int.TryParse(Console.ReadLine(), out parse2);
                            Console.WriteLine(orderItem.GetOrderItemByOrderAndProduct(parse, parse2));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;

                    case 7://הצגת כל הפריטים בהזמנה זו
                        Console.WriteLine("enter order ID");
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




