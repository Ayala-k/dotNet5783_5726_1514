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
        _orderItemsList.Remove((_orderItemsList.FirstOrDefault(item => item?.ID == orderItemID))
            ?? throw new EntityNotFoundException("order item not found"));
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
        IEnumerable<OrderItem?> orderItemsListCopy = new List<OrderItem?>();
        orderItemsListCopy = from OrderItem? orderItem in _orderItemsList
                             where (predict == null || predict(orderItem))
                             select orderItem;
        return orderItemsListCopy;
    }

    /// <summary>
    /// get specific product by condition
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public OrderItem GetByCondition(Func<OrderItem?, bool> predict)
    {
        return (_orderItemsList.FirstOrDefault(item => predict(item)))
     ?? throw new EntityNotFoundException("order item not found");
    }
}