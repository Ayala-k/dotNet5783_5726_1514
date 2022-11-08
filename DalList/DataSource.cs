using DO;
namespace Dal;

/// <summary>
/// Data managing
/// </summary>
public static class DataSource//internallll
{
    static DataSource()
    {
        Console.WriteLine("in data source ctor");
        s_Initialize();
    }

    static readonly int _randomNumber = new Random().Next();

    internal static class Config
    {
        internal static int _productsEmptyIndex = 0;
        internal static int _ordersEmptyIndex = 0;
        internal static int _ordersItemsEmptyIndex = 0;

        internal static int _serialNumberOrder = 100;
        internal static int _SerialNumberOrder
        { get { return ++_serialNumberOrder; } }

        internal static int _serialNumberOrderItems = 100;
        internal static int _SerialNumberOrderItems
        { get { return ++_serialNumberOrderItems; } }
    }

    #region arrays
    internal static Product[] _productsArr = new Product[50];
    internal static Order[] _ordersArr = new Order[100];
    internal static OrderItem[] _orderItemsArr = new OrderItem[200];
    #endregion

    #region add functions
    private static void addProducts(Product p, int index)
    {
        _productsArr[index] = p;
        Config._productsEmptyIndex++;
    }

    private static void addOrders(Order o, int index)
    {
        _ordersArr[index] = o;
        Config._ordersEmptyIndex++;
    }

    private static void addOrderItem(OrderItem oi, int index)
    {
        _orderItemsArr[index] = oi;
        Config._ordersItemsEmptyIndex++;
    }
    #endregion

    private static void s_Initialize()
    {
        Product p = new Product();
        Order o = new Order();
        OrderItem oi = new OrderItem();


        int[] ids = new int[] { 100000, 100001, 100002, 100003, 100004, 100005, 100006, 100007, 100008, 100009 };
        string[] names = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

        Categories[] categories = new Categories[] { Categories.cat1, Categories.cat1, Categories.cat2 };
        double[] prices = new double[] { 50, 56, 73.5, 100, 44, 32, 160, 10, 15.5, 70 };
        int[] inStock = new int[] { 500, 200, 10, 69, 32, 14, 48, 0, 321, 53 };

        for (int i = 0; i < 10; i++)
        {
            p.ID = ids[i];
            p.Name = names[i];
            p.Category = categories[i];
            p.Price = prices[i];
            p.InStock = inStock[i];
            addProducts(p, i);
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
            o.OrderDate = DateTime.Now - new TimeSpan(new Random().Next(20, 1000000000), new Random().Next(0, 24), new Random().Next(0, 60));
            if (i < 16)
                o.ShipDate = o.OrderDate + new TimeSpan(new Random().Next(2, 5), new Random().Next(0, 24), new Random().Next(0, 60));
            if (i < 12)
                o.DeliveryDate = o.ShipDate + new TimeSpan(new Random().Next(10, 30), new Random().Next(0, 24), new Random().Next(0, 60));
            addOrders(o, 0);
        }

        for (int i = 0; i < 40; i++)
        {
            oi.ID = Config._SerialNumberOrderItems;
            while (true)
            {
                int rand = new Random().Next(0, 9);
                oi.ProductID = _productsArr[rand].ID;
                oi.Price = _productsArr[rand].Price;
                for (int j = 0; j < i; j++)
                    if (_orderItemsArr[j].ProductID != oi.ProductID || _orderItemsArr[j].OrderID != oi.OrderID)
                        break;
            }
            while (true)
            {
                oi.OrderID = _ordersArr[new Random().Next(0, 19)].ID;
                int count = 0;
                for (int j = 0; j < i; j++)
                    if (_orderItemsArr[j].OrderID == oi.OrderID)
                        count++;
                if (count < 4)
                    break;
            }          
            oi.Amount = new Random().Next(1,5);
            addOrderItem(oi, 0);
        }
    }
}
