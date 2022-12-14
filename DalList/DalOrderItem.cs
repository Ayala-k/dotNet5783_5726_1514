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
            if (item?.ID == orderItemID)
            {
                _orderItemsList.Remove(item);
                return;
            }
        throw new EntityNotFoundException("order item not found");
    }

    /// <summary>
    /// update order item
    /// </summary>
    /// <param name="o">order item to update (by id)</param>
    public void Update(OrderItem oi)
    {
        for (var i = 0; i < _orderItemsList.Count; i++)
        {
            if (_orderItemsList[i]?.ID == oi.ID)
            {
                _orderItemsList[i] = oi;
                return;
            }
        }
        throw new EntityNotFoundException("order item not found");
    }

    /// <summary>
    /// get all order items
    /// </summary>
    /// <returns>array of order items</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
    {
        List<OrderItem?> orderItemsListCopy = new List<OrderItem?>();
        if (predict == null)
        {
            //orderItemsListCopy = _orderItemsList;
            foreach (OrderItem orderItem in _orderItemsList)
            {
                orderItemsListCopy.Add(orderItem);
            }
        }
        else
        {
            foreach (OrderItem orderItem in _orderItemsList)
            {
                if (predict(orderItem))
                    orderItemsListCopy.Add(orderItem);
            }
        }
        //IEnumerable<OrderItem?> newrdOerItemsListCopy = new List<OrderItem?>(orderItemsListCopy);
        return orderItemsListCopy;
    }

    /// <summary>
    /// get specific product by condition
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public OrderItem GetByCondition(Func<OrderItem, bool>? predict)
    {
        foreach (OrderItem orderItem in _orderItemsList)
        {
            if (predict(orderItem))
                return orderItem;
        }
        throw new EntityNotFoundException("order item not found");
    }

}