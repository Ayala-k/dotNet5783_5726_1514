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
  foreach (Order item in _ordersList)
   if (item.ID == orderID)
   {
    _ordersList.Remove(item);
    return;
   }
  throw new EntityNotFoundException("order not found");
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
 /// get order by id
 /// </summary>
 /// <param name="orderID">id of requested order</param>
 /// <returns>requested order</returns>
 /// <exception cref="System.Exception"></exception>
 //public Order Get(int orderID)
 //{
 // foreach (var item in _ordersList)
 //  if (item.ID == orderID)
 //  {
 //   return item;
 //  }
 // throw new EntityNotFoundException("order not found");
 //}

 /// <summary>
 /// get all orders
 /// </summary>
 /// <returns>array of orders</returns>
 public IEnumerable<Order> GetAll(Func<Order, bool>? predict = null)
 {
  List<Order> ordersListCopy=new List<Order>();
  if (predict == null)
  {
   foreach (Order order in _ordersList)
   {
     ordersListCopy.Add(order);
   }
  }
  else
  {
   foreach (Order order in _ordersList)
   {
    if (predict(order))
     ordersListCopy.Add(order);
   }
  }
  return ordersListCopy;
 }
 public Order GetByCondition(Func<Order, bool>? predict)
 {
  foreach (Order order in _ordersList)
  {
   if (predict(order))
    return order;
  }
  throw new EntityNotFoundException("order not found");
 }
}