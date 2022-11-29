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
 /// get order item by id
 /// </summary>
 /// <param name="orderItemID">id of requested order item</param>
 /// <returns>requested order item</returns>
 /// <exception cref="System.Exception"></exception>
 //public OrderItem Get(int orderItemID)
 //{
 // foreach (var item in _orderItemsList)
 //  if (item.ID == orderItemID)
 //  {
 //   return item;
 //  }
 // throw new EntityNotFoundException("order item not found");
 //}

 /// <summary>
 /// get all order items
 /// </summary>
 /// <returns>array of order items</returns>
 public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
 {
  List<OrderItem?> orderItemsListCopy=new List<OrderItem?>();
  if (predict == null)
  {
   orderItemsListCopy = _orderItemsList;
  }
  else
  {
   foreach (OrderItem? orderItem in _orderItemsList)
   {
    if (predict(orderItem))
     orderItemsListCopy.Add(orderItem);
   }
  }
  IEnumerable<OrderItem?> newrdOerItemsListCopy = new List<OrderItem?>(orderItemsListCopy);
  return newrdOerItemsListCopy;
 }

 public OrderItem GetByCondition(Func<OrderItem, bool>? predict)
 {
  foreach (OrderItem orderItem in _orderItemsList)
  {
   if (predict(orderItem))
    return orderItem;
  }
  throw new EntityNotFoundException("order item not found");

 }
 /// <summary>
 /// finding an order item by order and product
 /// </summary>
 /// <param name="oID">order id</param>
 /// <param name="pID">product id</param>
 /// <returns>order item with these IDs</returns>
 /// <exception cref="System.Exception"></exception>
 /// 

 //public OrderItem GetOrderItemByOrderAndProduct(int oID, int pID)
 //{
 // foreach (var item in _orderItemsList)
 //  if (item.OrderID == oID && item.ProductID == pID)
 //   return item;
 // throw new EntityNotFoundException("order item not found");
 //}

 /// <summary>
 /// get all order items of an order
 /// </summary>
 /// <param name="orderID">requested order id</param>
 /// <returns>array of order items of requested order</returns>
 /// 
 //public IEnumerable<OrderItem> GetOrderItemsByOrder(int orderID)
 //{
 // List<OrderItem> ordersItemsList = new List<OrderItem>();
 // foreach (var item in _orderItemsList)
 //  if (item.OrderID == orderID)
 //   ordersItemsList.Add(item);
 // return ordersItemsList;
 //}
}