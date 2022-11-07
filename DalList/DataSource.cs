using DO;
namespace Dal;

/// <summary>
/// Data managing
/// </summary>
internal static class DataSource
{
    static DataSource()
    {
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
    }

    private static void addOrders(Order o, int index)
    {
        _ordersArr[index] = o;
    }

    private static void addOrderItem(OrderItem oi, int index)
    {
        _orderItemsArr[index] = oi;
    }
    #endregion

    private static void s_Initialize()
    {
        Product p = new Product();
        Order o = new Order();
        OrderItem oi = new OrderItem();

        p.ID = 100000;
        p.Name = "prod1";
        p.Price = 2.5;
        p.InStock = 70;
        addProducts(p, 0);//in for

        o.ID = Config._SerialNumberOrder;
        Config._serialNumberOrder++;
        o.CustomerName = "tamar";
        o.CustomerEmail = "@";
        o.CustomerAddress = "fdijhvuydfhiofkdpcd";
        o.OrderDate = DateTime.Now;
        o.ShipDate = DateTime.Now;
        o.DeliveryDate = DateTime.Now;
        addOrders(o, 0);

        oi.ID = Config._SerialNumberOrderItems;
        Config._serialNumberOrderItems++;
        oi.ProductID = 100000;
        oi.OrderID = Config._SerialNumberOrder - 1;
        oi.Price = 5;
        oi.Amount = 6;
        addOrderItem(oi, 0);

        //addProducts(p);
        //addOrders(o);
        //addOrderItem(oi);
    }
}
