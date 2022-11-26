using BL.BlApi;
using Dal;
using DalApi;

namespace BL.BlImplementation;

internal class Order : BlApi.IOrder
{
 IDal Dal = new DalList();
 public IEnumerable<BO.OrderForList> GetOrders()
 {
  IEnumerable<DO.Order> ordersListDal = Dal.Order.GetAll();
  List<BO.OrderForList> orderListBL = new List<BO.OrderForList>();
  foreach (DO.Order orderDal in ordersListDal)
  {
   BO.OrderForList order = new BO.OrderForList()
   {
    ID = orderDal.ID,
    CustomerName = orderDal.CustomerName,
    Status = findStatus(orderDal),
    AmountOfItems = findAmountOfItems(orderDal.ID),
    TotalPrice = findTotalPrice(orderDal.ID),
   };
   orderListBL.Add(order);
  }
  return orderListBL;
 }

 public BO.Order GetOrderDetails(int orderID)
 {
  if (orderID > 0)
  {
   DO.Order orderDal = new DO.Order();
   try
   {
    orderDal = Dal.Order.Get(orderID);
   }
   catch (DO.EntityNotFoundException e)
   {
    //throw new Exception(e);
   }

   IEnumerable<DO.OrderItem> OrderItemsDal = new List<DO.OrderItem>();
   try
   {
    OrderItemsDal = Dal.OrderItem.GetOrderItemsByOrder(orderID);
   }
   catch (DO.EntityNotFoundException e)
   {
    //throw new Exception(e);
   }
   BO.Order orderBL = new BO.Order()
   {
    ID = orderDal.ID,
    CustomerName = orderDal.CustomerName,
    CustomerEmail = orderDal.CustomerEmail,
    CustomerAddress = orderDal.CustomerAddress,
    Status = findStatus(orderDal),
    PaymentDate = orderDal.OrderDate,
    ShipDate = orderDal.ShipDate,
    DeliveryDate = orderDal.DeliveryDate,
    ItemsList = getOrderItem(OrderItemsDal),
    TotalPrice = findTotalPrice(orderDal.ID)
   };
   return orderBL;
  }
  else
   throw new Exception();
 }
 public BO.Order UpdateOrderShipping(int orderID)
 {

  DO.Order orderDal = Dal.Order.Get(orderID);

  if (orderDal.ShipDate == default(DateTime))
  {
   orderDal.ShipDate = DateTime.Now;
   try
   {
    Dal.Order.Update(orderDal);
   }
   catch
   {
    throw new Exception();
   }

   IEnumerable<DO.OrderItem> OrderItemsDal = new List<DO.OrderItem>();
   try
   {
    OrderItemsDal = Dal.OrderItem.GetOrderItemsByOrder(orderID);
   }
   catch (DO.EntityNotFoundException e)
   {
    //throw new Exception(e);
   }

   BO.Order orderBL = new BO.Order()
   {
    ID = orderDal.ID,
    CustomerName = orderDal.CustomerName,
    CustomerEmail = orderDal.CustomerEmail,
    CustomerAddress = orderDal.CustomerAddress,
    Status = findStatus(orderDal),
    PaymentDate = orderDal.OrderDate,
    ShipDate = orderDal.ShipDate,
    DeliveryDate = orderDal.DeliveryDate,
    ItemsList = getOrderItem(OrderItemsDal),
    TotalPrice = findTotalPrice(orderDal.ID)
   };
   return orderBL;
  }
  else
   throw new Exception();
 }
 public BO.Order UpdateOrderDelivering(int orderID)
 {

  DO.Order orderDal = Dal.Order.Get(orderID);

  if (orderDal.DeliveryDate == default(DateTime)
      && orderDal.ShipDate < DateTime.Now)
  {
   orderDal.DeliveryDate = DateTime.Now;
   try
   {
    Dal.Order.Update(orderDal);
   }
   catch
   {
    throw new Exception();
   }

   IEnumerable<DO.OrderItem> OrderItemsDal = new List<DO.OrderItem>();
   try
   {
    OrderItemsDal = Dal.OrderItem.GetOrderItemsByOrder(orderID);
   }
   catch (DO.EntityNotFoundException e)
   {
    //throw new Exception(e);
   }

   BO.Order orderBL = new BO.Order()
   {
    ID = orderDal.ID,
    CustomerName = orderDal.CustomerName,
    CustomerEmail = orderDal.CustomerEmail,
    CustomerAddress = orderDal.CustomerAddress,
    Status = findStatus(orderDal),
    PaymentDate = orderDal.OrderDate,
    ShipDate = orderDal.ShipDate,
    DeliveryDate = orderDal.DeliveryDate,
    ItemsList = getOrderItem(OrderItemsDal),
    TotalPrice = findTotalPrice(orderDal.ID)
   };
   return orderBL;
  }
  else
   throw new Exception();
 }
 public BO.OrderTracking OrderTrack(int orderID)
 {
  DO.Order orderDal = new DO.Order();
  try
  {
   orderDal = Dal.Order.Get(orderID);
  }
  catch
  {
   /////////////////////////////////////
  }
  List<BO.OrderTracking.DateAndProgressDescription> dateAndProgressDescriptionsList = new List<BO.OrderTracking.DateAndProgressDescription>();
  BO.OrderTracking.DateAndProgressDescription dateAndProgressDescription = new BO.OrderTracking.DateAndProgressDescription();
  OrderStatus status = findStatus(orderDal);
  if (status == OrderStatus.OrderCommited || status == OrderStatus.OrderShipped
      || status == OrderStatus.OrderDelivered)
  {
   dateAndProgressDescription.ProgressDate = orderDal.OrderDate;
   dateAndProgressDescription.ProgressDescription = "order commited";
   dateAndProgressDescriptionsList.Add(dateAndProgressDescription);
  }
  if (status == OrderStatus.OrderShipped || status == OrderStatus.OrderDelivered)
  {
   dateAndProgressDescription.ProgressDate = orderDal.ShipDate;
   dateAndProgressDescription.ProgressDescription = "order shipped";
   dateAndProgressDescriptionsList.Add(dateAndProgressDescription);
  }
  if (status == OrderStatus.OrderDelivered)
  {
   dateAndProgressDescription.ProgressDate = orderDal.DeliveryDate;
   dateAndProgressDescription.ProgressDescription = "order delivered";
   dateAndProgressDescriptionsList.Add(dateAndProgressDescription);
  }


  BO.OrderTracking orderTracking = new BO.OrderTracking()
  {
   ID = orderID,
   Status = status,
   DateAndProgressDescriptionsList = dateAndProgressDescriptionsList
  };
  return orderTracking;
 }
 public BO.Order UpdateOrder(int orderID,int productID,int newAmount)
 {
  DO.Order orderDal = Dal.Order.Get(orderID);
  IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll();
  List<DO.OrderItem> orderItemsList = new List<DO.OrderItem>(orderItems);
  for (var i=0;i<orderItemsList.Count;i++)
  {
   if (orderItemsList[i].OrderID == orderID&& orderItemsList[i].ProductID==productID)
   {
    if (Dal.Order.Get(orderID).ShipDate <= DateTime.Now)
     throw new Exception();//נשלח כבר
    DO.OrderItem newItem = orderItemsList[i];
    newItem.Amount = newAmount;
    Dal.OrderItem.Update(newItem);

    //BO.Order orderBL = new BO.Order()
    //{
    // ID = orderID,
    // CustomerName = orderDal.CustomerName,
    // CustomerEmail = orderDal.CustomerEmail,
    // CustomerAddress = orderDal.CustomerAddress,
    // Status = findStatus(orderDal),
    // PaymentDate = orderDal.OrderDate,
    // ShipDate = orderDal.ShipDate,
    // DeliveryDate = orderDal.DeliveryDate,
    // ItemsList = getOrderItem(OrderItemsDal),
    // TotalPrice = findTotalPrice(orderDal.ID)
    //};
    
    //צריך להחזיר?
   }
  }

 }

 private OrderStatus findStatus(DO.Order order)
 {
  OrderStatus status = OrderStatus.OrderCommited;
  if (order.ShipDate < DateTime.Now)
   status = OrderStatus.OrderShipped;
  if (order.DeliveryDate < DateTime.Now)
   status = OrderStatus.OrderDelivered;
  return status;
 }
 private int findAmountOfItems(int orderID)
 {
  IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll();
  int amountOfItems = 0;
  foreach (DO.OrderItem oi in orderItems)
  {
   if (oi.OrderID == orderID)
   {
    amountOfItems++;
   }
  }
  return amountOfItems;
 }
 private double findTotalPrice(int orderID)
 {
  IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll();
  double totalPrice = 0;
  foreach (DO.OrderItem oi in orderItems)
  {
   if (oi.OrderID == orderID)
   {
    totalPrice += oi.Price * oi.Amount;
   }
  }
  return totalPrice;
 }
 private List<BO.OrderItem> getOrderItem(IEnumerable<DO.OrderItem> OrderItemsDal)
 {
  List<BO.OrderItem> OrderItemsBL = new List<BO.OrderItem>();
  foreach (DO.OrderItem oi in OrderItemsDal)
  {
   BO.OrderItem orderItemBL = new BO.OrderItem()
   {
    ID = oi.ID,
    Name = Dal.Product.Get(oi.ProductID).Name,//try
    ProductID = oi.ProductID,
    Price = oi.Price,
    Amount = oi.Amount,
    TotalPrice = oi.Price * oi.Amount
   };
   OrderItemsBL.Add(orderItemBL);
  }
  return OrderItemsBL;
 }

}
