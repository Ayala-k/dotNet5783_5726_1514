using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// accesing OrderItem
/// </summary>
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// adding order item
    /// </summary>
    /// <param name="o">order item to add</param>
    /// <returns>id of the added order item</returns>
    public int Add(OrderItem oi)
    {
        oi.ID = Config._SerialNumberOrder;
        _orderItemsList.Add(oi);
        return oi.ID;
    }

    /// <summary>
    /// deleting order item
    /// </summary>
    /// <param name="orderItemID">order item id to delete</param>
    public void Delete(int orderItemID)
    {
        foreach (var item in _orderItemsList)
            if (item.ID == orderItemID)
            {
                _orderItemsList.Remove(item);
                break;
            }
    }

    /// <summary>
    /// update order item
    /// </summary>
    /// <param name="o">order item to update (by id)</param>
    public void Update(OrderItem oi)
    {
        for (var i = 0; i < _orderItemsList.Count; i++)
        {
            if (_orderItemsList[i].ID == oi.ID)
            {
                _orderItemsList[i] = oi;
                return;
            }
        }
        throw new Exception();
    }

    /// <summary>
    /// get order item by id
    /// </summary>
    /// <param name="orderItemID">id of requested order item</param>
    /// <returns>requested order item</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(int orderItemID)
    {
        foreach (var item in _orderItemsList)
            if (item.ID == orderItemID)
            {
                return item;
            }
        throw new Exception();
    }

    /// <summary>
    /// get all order items
    /// </summary>
    /// <returns>array of order items</returns>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> orderItemsListCopy = _orderItemsList;
        return orderItemsListCopy;
    }

    /// <summary>
    /// finding an order item by order and product
    /// </summary>
    /// <param name="oID">order id</param>
    /// <param name="pID">product id</param>
    /// <returns>order item with these IDs</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItemByOrderAndProduct(int oID, int pID)
    {
        foreach (var item in _orderItemsList)
            if (item.OrderID == oID && item.ProductID == pID)
                return item;
        throw new Exception();
    }

    /// <summary>
    /// get all order items of an order
    /// </summary>
    /// <param name="orderID">requested order id</param>
    /// <returns>array of order items of requested order</returns>
    public IEnumerable<OrderItem> GetOrderItemsByOrder(int orderID)
    {
        List<OrderItem> ordersItemsList = new List<OrderItem>();
        foreach (var item in _orderItemsList)
            if (item.OrderID == orderID)
                ordersItemsList.Add(item);
        return ordersItemsList;
    }
}

/*using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// accesing OrderItem
/// </summary>
internal class DalOrderItem:IOrderItem
{
    /// <summary>
    /// adding order item
    /// </summary>
    /// <param name="o">order item to add</param>
    /// <returns>id of the added order item</returns>
    public int AddOrderItem(OrderItem o)
    {
        o.ID = Config._SerialNumberOrderItems;
        _orderItemsArr[Config._ordersItemsEmptyIndex] = o;
        Config._ordersItemsEmptyIndex++;
        return o.ID;
    }

    /// <summary>
    /// deleting order item
    /// </summary>
    /// <param name="orderItemID">order item id to delete</param>
    public void DeleteOrderItem(int orderItemID)
    {
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            if (orderItemID == _orderItemsArr[i].ID)
            {
                _orderItemsArr[i] = _orderItemsArr[Config._ordersItemsEmptyIndex - 1];
                Config._ordersItemsEmptyIndex--;
                break;
            }
    }

    /// <summary>
    /// update order item
    /// </summary>
    /// <param name="o">order item to update (by id)</param>
    public void UpdateOrderItem(OrderItem o)
    {
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            if (o.ID == _orderItemsArr[i].ID)
                _orderItemsArr[i] = o;
    }

    /// <summary>
    /// get order item by id
    /// </summary>
    /// <param name="orderItemID">id of requested order item</param>
    /// <returns>requested order item</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItem(int orderItemID)
    {
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            if (orderItemID == _orderItemsArr[i].ID)
                return _orderItemsArr[i];
        throw new Exception("order item does not exist");
    }

    /// <summary>
    /// get all order items
    /// </summary>
    /// <returns>array of order items</returns>
    public OrderItem[] GetAllOrderItems()
    {
        OrderItem[] _orderItemsCopy = new OrderItem[Config._ordersItemsEmptyIndex];
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            _orderItemsCopy[i] = _orderItemsArr[i];
        return _orderItemsCopy;
    }

    /// <summary>
    /// finding an order item by order and product
    /// </summary>
    /// <param name="oID">order id</param>
    /// <param name="pID">product id</param>
    /// <returns>order item with these IDs</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItemByOrderAndProduct(int oID, int pID)
    {
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            if ((oID == _orderItemsArr[i].OrderID) && (pID == _orderItemsArr[i].ProductID))
                return _orderItemsArr[i];
        throw new Exception("order item does not exist");
    }

    /// <summary>
    /// get all order items of an order
    /// </summary>
    /// <param name="orderID">requested order id</param>
    /// <returns>array of order items of requested order</returns>
    public OrderItem[] GetOrderItemsByOrder(int orderID)
    {
        int index = 0;
        int count = 0;
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            if (orderID == _orderItemsArr[i].OrderID)
                count++;
        OrderItem[] _orderItemsByOrderArr = new OrderItem[count];
        for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
            if (orderID == _orderItemsArr[i].OrderID)
            {
                _orderItemsByOrderArr[index] = _orderItemsArr[i];
                index++;
            }
        return _orderItemsByOrderArr;
    }

}

*/
