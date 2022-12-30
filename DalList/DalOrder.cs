using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// accesing Order
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// adding order
    /// </summary>
    /// <param name="order">order to add</param>
    /// <returns>id of added order</returns>
    public int Add(Order order)
    {
        order.ID = Config._SerialNumberOrder;
        _ordersList.Add(order);
        return order.ID;
    }

    /// <summary>
    /// deleting order
    /// </summary>
    /// <param name="orderID">id of order to delete</param>
    public void Delete(int orderID)
    {
        _ordersList.Remove((_ordersList.FirstOrDefault(item => item?.ID == orderID))
            ?? throw new EntityNotFoundException("order not found"));
    }

    /// <summary>
    /// updating order
    /// </summary>
    /// <param name="order">order to update (by id)</param>
    public void Update(Order order)
    {
        for (var i = 0; i < _ordersList.Count; i++)
        {
            if (_ordersList[i]?.ID == order.ID)
            {
                _ordersList[i] = order;
                return;
            }
        }
        throw new EntityNotFoundException("order not found");
    }

    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns>array of orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? predict = null)
    {
        IEnumerable<Order?> ordersListCopy = new List<Order?>();
        ordersListCopy = from Order? order in _ordersList
                         where (predict == null || predict(order))
                         select order;
        return ordersListCopy;
    }

    /// <summary>
    /// get specific order by condition
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Order GetByCondition(Func<Order?, bool> predict)
    {
        return (_ordersList.FirstOrDefault(item => predict(item)))
            ?? throw new EntityNotFoundException("order not found");
    }
}