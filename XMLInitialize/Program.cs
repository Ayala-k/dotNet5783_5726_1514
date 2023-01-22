﻿using DO;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Product = DO.Product;

public class Sample
{
    internal static List<Product?> _productsList = new List<Product?>();
    internal static List<Order?> _ordersList = new List<Order?>();
    internal static List<OrderItem?> _orderItemsList = new List<OrderItem?>();
    internal static Cart userCart = new Cart();
    internal static List<Order?> userOrdersList = new List<Order?>();

    public static void Main()
    {
        //List<BL.BO.Order> list = new List<BL.BO.Order>();
        //BL.BO.Order order = new BL.BO.Order()
        //{
        //    ID = 101
        //};
        //list.Add(order);
        //FileStream fs = new FileStream(@"C:\Users\Kluft\source\repos\Ayala-k\dotNet5783_5726_1514\xml\XMLOrder.xml", FileMode.OpenOrCreate);
        //XmlSerializer xs = new XmlSerializer(typeof(List<BL.BO.Order>));
        //xs.Serialize(fs, list);
        //fs.Close();

        s_Initialize();

    }
    private static void s_Initialize()
    {
        Product p = new Product();
        Order o = new Order();
        OrderItem oi = new OrderItem();

        int[] ids = new int[] { 100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009 };
        string[] names = new string[] { "classic guitar", "Grand piano", "Electric piano", "Electric Guitar", " violin", "cello", "recorder", "Saxophone", "Single drum", "Electronic drums" };

        Categories[] categories = new Categories[] { Categories.guitars, Categories.keyboards,
            Categories.keyboards, Categories.guitars, Categories.bowTool,
            Categories.bowTool, Categories.WindInstruments, Categories.WindInstruments,
            Categories.percussions, Categories.percussions };
        double[] prices = new double[] { 30000, 4000, 2605, 900, 2083, 2129, 58, 2188, 1559, 2385 };
        int[] inStock = new int[] { 2, 200, 10, 69, 32, 14, 48, 0, 321, 53 };

        for (int i = 0; i < 10; i++)
        {
            p.ID = ids[i];
            p.Name = names[i];
            p.Category = categories[i];
            p.Price = prices[i];
            p.InStock = inStock[i];
            _productsList.Add(p);
        }

        string[] customerNames = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        string[] customerEmails = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        string[] customerAddress = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

        for (int i = 0; i < 20; i++)
        {
            o.ID = 100 + i;
            o.CustomerName = customerNames[i];
            o.CustomerEmail = customerEmails[i];
            o.CustomerAddress = customerAddress[i];
            o.OrderDate = DateTime.Now - new TimeSpan(new Random().Next(20, 100000),
                new Random().Next(0, 24), new Random().Next(0, 60));
            if (i < 16)
                o.ShipDate = o.OrderDate + new TimeSpan(new Random().Next(2, 5),
                    new Random().Next(0, 24), new Random().Next(0, 60));
            if (i < 12)
                o.DeliveryDate = null;
            //o.DeliveryDate = o.ShipDate + new TimeSpan(new Random().Next(10, 30),
            //    new Random().Next(0, 24), new Random().Next(0, 60));
            _ordersList.Add(o);
        }

        int[] orderIDs = new int[] {101,101,102,102,102,103,104,104,104,104
            ,105,105,106,106,106,107,108,109,110,110
            ,110,110,111,112,112,113,114,114,115,116,
            116,116,117,117,117,118,119,120,120,120 };
        int[] productIDs = new int[] { 100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009,
            100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009,
            100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009,
            100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009 };

        for (int i = 0; i < 40; i++)
        {
            oi.ID = 100 + i;
            oi.ProductID = productIDs[i];
            oi.OrderID = orderIDs[i];
            oi.Amount = new Random().Next(1, 5);
            oi.Price = prices[i % 10];
            _orderItemsList.Add(oi);
        }

        FileStream fs = new FileStream(@"C:\Users\Kluft\source\repos\Ayala-k\dotNet5783_5726_1514\xml\XMLOrder.xml", FileMode.OpenOrCreate);
        XmlSerializer xs = new XmlSerializer(typeof(List<Order?>));
        xs.Serialize(fs, _ordersList);
        fs.Close();
        fs = new FileStream(@"C:\Users\Kluft\source\repos\Ayala-k\dotNet5783_5726_1514\xml\XMLProduct.xml", FileMode.OpenOrCreate);
        xs = new XmlSerializer(typeof(List<Product?>));
        xs.Serialize(fs, _productsList);
        fs.Close();
        fs = new FileStream(@"C:\Users\Kluft\source\repos\Ayala-k\dotNet5783_5726_1514\xml\XMLOrderItem.xml", FileMode.OpenOrCreate);
        xs = new XmlSerializer(typeof(List<OrderItem?>));
        xs.Serialize(fs, _orderItemsList);
        fs.Close();
    }
}




//private static void addProducts(Product p)
//{
//    _productsList.Add(p);
//}

//private static void addOrders(Order o)
//{
//    _ordersList.Add(o);

//}

//private static void addOrderItem(OrderItem oi)
//{
//    _orderItemsList.Add(oi);

//}
//#endregion




