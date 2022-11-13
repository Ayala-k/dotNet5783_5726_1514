using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// accesing Order
/// </summary>
internal class DalOrder:IOrder
{
    /// <summary>
    /// adding order
    /// </summary>
    /// <param name="order">order to add</param>
    /// <returns>id of added order</returns>
    public int AddOrder(Order order)
    {
        order.ID = Config._SerialNumberOrder;
        _ordersArr[Config._ordersEmptyIndex] = order;
        Config._ordersEmptyIndex++;
        return order.ID;
    }

    /// <summary>
    /// deleting order
    /// </summary>
    /// <param name="orderID">id of order to delete</param>
    public void DeleteOrder(int orderID)
    {
        for (int i = 0; i < Config._ordersEmptyIndex; i++)
            if (orderID == _ordersArr[i].ID)
            {
                _ordersArr[i] = _ordersArr[Config._ordersEmptyIndex - 1];
                Config._ordersEmptyIndex--;
                break;
            }
    }

    /// <summary>
    /// updating order
    /// </summary>
    /// <param name="order">order to update (by id)</param>
    public void UpdateOrder(Order order)
    {
        for (int i = 0; i < Config._ordersEmptyIndex; i++)
            if (order.ID == _ordersArr[i].ID)
                _ordersArr[i] = order;
    }

    /// <summary>
    /// get order by id
    /// </summary>
    /// <param name="orderID">id of requested order</param>
    /// <returns>requested order</returns>
    /// <exception cref="Exception"></exception>
    public Order GetOrder(int orderID)
    {
        for (int i = 0; i < Config._ordersEmptyIndex; i++)
            if (orderID == _ordersArr[i].ID)
                return _ordersArr[i];
        throw new Exception("order does not exist");
    }

    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>array of orders</returns>
    public Order[] GetAllOrders()
    {
        Order[] _ordersCopy = new Order[Config._ordersEmptyIndex ];
        for (int i = 0; i < Config._ordersEmptyIndex; i++)
            _ordersCopy[i] = _ordersArr[i];
        return _ordersCopy;
    }

}
