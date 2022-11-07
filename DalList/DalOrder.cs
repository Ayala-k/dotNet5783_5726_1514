﻿using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
 public void addOrder(Order o)
 {
  o.ID = Config._SerialNumberOrder;
  Config._serialNumberOrder++;
  for (int i = 0; i != Config._ordersEmptyIndex; i++)
  {
   if (o.ID == _ordersArr[i].ID)
    return;
  }
  _ordersArr[Config._ordersEmptyIndex] = o;
  Config._ordersEmptyIndex++;
 }

 public void deleteOrder(int orderID)
 {
        for (int i = 0; i < Config._ordersEmptyIndex; i++)
            if (orderID == _ordersArr[i].ID)
            {
                _ordersArr[i] = _ordersArr[Config._ordersEmptyIndex - 1];
                Config._ordersEmptyIndex--;
            }
    }

 public void updateOrder(Order o)
 {

  for (int i = 0; i < Config._ordersEmptyIndex; i++)
   if (o.ID == _ordersArr[i].ID)
    _ordersArr[i] = o;
 }

 public Order getOrder(int orderID)
 {
  for (int i = 0; i < Config._ordersEmptyIndex; i++)
   if (orderID == _ordersArr[i].ID)
    return _ordersArr[i];
  return null;
 }

 public Order[] getAllOrders()
    {
        Order[] _ordersCopy = new Order[Config._ordersEmptyIndex - 1];
        for (int i = 0; i < Config._ordersEmptyIndex - 1; i++)
            _ordersCopy[i] = _ordersArr[i];
        return _ordersCopy;
    }
}
