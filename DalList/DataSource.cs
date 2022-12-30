using DO;
namespace Dal;

/// <summary>
/// Data managing
/// </summary>
internal static class DataSource
{
    static DataSource()
    {
        Console.WriteLine("in data source ctor");
        s_Initialize();
    }

    static readonly int _randomNumber = new Random().Next();

    internal static class Config
    {
        internal static int _serialNumberOrder = 100;
        internal static int _SerialNumberOrder
        { get { return ++_serialNumberOrder; } }

        internal static int _serialNumberOrderItems = 100;
        internal static int _SerialNumberOrderItems
        { get { return ++_serialNumberOrderItems; } }
    }

    #region lists
    internal static List<Product?> _productsList = new List<Product?>();
    internal static List<Order?> _ordersList = new List<Order?>();
    internal static List<OrderItem?> _orderItemsList = new List<OrderItem?>();
    #endregion

    #region add functions
    private static void addProducts(Product p)
    {
        _productsList.Add(p);
    }

    private static void addOrders(Order o)
    {
        _ordersList.Add(o);

    }

    private static void addOrderItem(OrderItem oi)
    {
        _orderItemsList.Add(oi);

    }
    #endregion

    private static void s_Initialize()
    {
        Product p = new Product();
        Order o = new Order();
        OrderItem oi = new OrderItem();

        int[] ids = new int[] { 100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009 };
        string[] names = new string[] { "Grand piano", "Electric piano", "classic guitar", "Electric Guitar", " violin", "cello", "recorder", "Saxophone", "Single drum", "Electronic drums" };

        Categories[] categories = new Categories[] { Categories.keyboards, Categories.keyboards,
            Categories.guitars, Categories.guitars, Categories.bowTool,
            Categories.bowTool, Categories.WindInstruments, Categories.WindInstruments,
            Categories.percussions, Categories.percussions };
        double[] prices = new double[] { 30000, 4000, 2605, 900, 2083, 2129, 58, 2188, 1559, 2385 };
        int[] inStock = new int[] { 500, 200, 10, 69, 32, 14, 48, 0, 321, 53 };

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
            o.ID = Config._SerialNumberOrder;
            o.CustomerName = customerNames[i];
            o.CustomerEmail = customerEmails[i];
            o.CustomerAddress = customerAddress[i];
            o.OrderDate = DateTime.Now - new TimeSpan(new Random().Next(20, 100000),
                new Random().Next(0, 24), new Random().Next(0, 60));
            if (i < 16)
                o.ShipDate = o.OrderDate + new TimeSpan(new Random().Next(2, 5),
                    new Random().Next(0, 24), new Random().Next(0, 60));
            if (i < 12)
                o.DeliveryDate = o.ShipDate + new TimeSpan(new Random().Next(10, 30),
                    new Random().Next(0, 24), new Random().Next(0, 60));
            _ordersList.Add(o);
        }

        int[] orderIDs = new int[] {101,101,102,102,102,103,104,104,104,104
            ,105,105,106,106,106,107,108,109,110,110
            ,110,110,111,112,112,113,114,114,115,116,
            116,116,117,117,117,118,119,120,120,120 };
        int[] productIDs = new int[] { 100000, 100002, 100000, 100008, 100009, 100005, 100003, 100002, 100007, 100008,
            100007, 100004, 100005, 100006, 100008, 100009, 100004, 100003, 100002, 100001
            , 100008, 100003, 100008, 100008, 100004, 100005, 100005, 100007, 100008, 100005
            , 100007, 100008, 100002, 100003, 100008, 100009, 100003, 100006, 100009, 100001 };

        for (int i = 0; i < 40; i++)
        {
            oi.ID = Config._SerialNumberOrderItems;
            oi.ProductID = productIDs[i];
            oi.OrderID = orderIDs[i];
            oi.Amount = new Random().Next(1, 5);
            _orderItemsList.Add(oi);
        }
    }
}


