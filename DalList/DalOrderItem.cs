
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
 public int addOrderItem(OrderItem o)
 {
  o.ID = Config._SerialNumberOrderItems;
  _orderItemsArr[Config._ordersItemsEmptyIndex] = o;
  Config._ordersItemsEmptyIndex++;
  return o.ID;
 }

 public void deleteOrderItem(int orderItemID)
 {
  for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
   if (orderItemID == _orderItemsArr[i].ID)
   {
    _orderItemsArr[i] = _orderItemsArr[Config._ordersItemsEmptyIndex - 1];
    Config._ordersItemsEmptyIndex--;
   }
 }

 public void updateOrderItem(OrderItem o)
 {

  for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
   if (o.ID == _orderItemsArr[i].ID)
    _orderItemsArr[i] = o;
 }

 public OrderItem getOrderItem(int orderItemID)
 {
  for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
   if (orderItemID == _orderItemsArr[i].ID)
    return _orderItemsArr[i];
  throw new Exception("order item does not exist");
 }

 public OrderItem getOrderItemByOrderAndProduct(int oID, int pID)
 {
  for (int i = 0; i < Config._ordersItemsEmptyIndex; i++)
   if ((oID == _orderItemsArr[i].OrderID) && (pID == _orderItemsArr[i].ProductID))
    return _orderItemsArr[i];
        throw new Exception("order item does not exist");

 }

 public OrderItem[] getOrderItemsByOrder(int orderID)
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

 public OrderItem[] getAllOrderItems()
 {
  OrderItem[] _orderItemsCopy = new OrderItem[Config._ordersItemsEmptyIndex - 1];
  for (int i = 0; i < Config._ordersItemsEmptyIndex - 1; i++)
   _orderItemsCopy[i] = _orderItemsArr[i];
  return _orderItemsCopy;
 }

}

