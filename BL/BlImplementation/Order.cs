
using BL.BlApi;
using Dal;
using DalApi;


namespace BL.BlImplementation;

internal class Order : BlApi.IOrder
{
    IDal Dal = new DalList();
 IEnumerable<BO.OrderForList> GetOrders()
 {
  IEnumerable<DO.Order> orderssListDal = Dal.Order.GetAll();
  List<BO.OrderForList> orderListBL = new List<BO.OrderForList>();
  OrderStatus status;
  int amountOfItems ;
  double totalPrice;

  foreach (DO.Order orderDal in orderssListDal)
  {
   if (orderDal.ShipDate>DateTime.Now)
    status = OrderStatus.OrderCommited;
   else if(orderDal.DeliveryDate<DateTime.Now)
    status = OrderStatus.OrderDelivered;
   else
    status = OrderStatus.OrderShipped;
   IEnumerable<DO.OrderItem> orderItems =Dal.OrderItem.GetAll();
   amountOfItems = 0;
   totalPrice = 0;
   foreach (DO.OrderItem oi in orderItems)
   {
    if(oi.OrderID==orderDal.ID)
    {
     amountOfItems++;
     totalPrice += oi.Price;
    }
   }

    BO.OrderForList order = new BO.OrderForList()
   {
    ID = orderDal.ID,
    CustomerName = orderDal.CustomerName,
    Status = status,
    AmountOfItems = amountOfItems,
    TotalPrice = totalPrice,

   };
   orderListBL.Add(order);
  }
  return orderListBL;
 }

 BO.Order GetOrderDetails(int orderID)
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

   IEnumerable<DO.OrderItem> OrderItemsDal= new List<DO.OrderItem>();//אם זה לא ליסט זה עושה שגיאה
   try
   {
    OrderItemsDal = Dal.OrderItem.GetOrderItemsByOrder(orderID);
   }
   catch (DO.EntityNotFoundException e)
   {
    //throw new Exception(e);
   }
   OrderStatus status;
   double totalPrice = 0;
   if (orderDal.ShipDate > DateTime.Now)
    status = OrderStatus.OrderCommited;
   else if (orderDal.DeliveryDate < DateTime.Now)
    status = OrderStatus.OrderDelivered;
   else
    status = OrderStatus.OrderShipped;

   foreach (DO.OrderItem oi in OrderItemsDal)
   {
    totalPrice+=oi.Price;
   }
   List<BO.OrderItem> OrderItemsBL = new List<BO.OrderItem>();
   foreach (BO.OrderItem oi in OrderItemsBL)
   {
    BO.OrderItem orderItemBL = new BO.OrderItem()
    {
     ID = oi.ID,
     Name = oi.Name,
     ProductID = oi.ProductID,
     Price = oi.Price,
     Amount = oi.Amount,
     TotalPrice = oi.TotalPrice
    };
    OrderItemsBL.Add(orderItemBL);
    }

   BO.Order orderBL = new BO.Order()
   {
    ID = orderDal.ID,
    CustomerName = orderDal.CustomerName,
    CustomerEmail = orderDal.CustomerEmail,
    CustomerAddress = orderDal.CustomerAddress,
    //public DateTime OrderDate { get; set; }
    Status = status,
    PaymentDate = orderDal.OrderDate,
    ShipDate = orderDal.ShipDate,
    DeliveryDate = orderDal.DeliveryDate,
    ItemsList = OrderItemsBL,
    TotalPrice = totalPrice
   };
  }
 }
 void UpdateOrderShipping(int orderID)
 {
  IEnumerable<DO.Order> orderListDal = Dal.Order.GetAll();
  using(var order= orderListDal.GetEnumerator())
  {
   while(order.MoveNext())
   {
    if (order.ID == orderID)
     if (order.ShipDate > DateTime.Now)
     {
      order.ShipDate = DateTime.Now;//לעדכן למה?
     }
     else
     //throw Exception
   else
    //throw Exception

   }
  }
  //for (var i= 0;i<orderListDal.Count();i++)
  //{
  // if (orderListDal.ElementAt(i).ID == orderID)
  //  if (orderListDal.ElementAt(i).ShipDate > DateTime.Now)
  //  {
  //   orderListDal.ElementAt(i).ShipDate = DateTime.Now;//לעדכן למה?
  //  }
  //  else
  //   //throw Exception
  // else
  //  //throw Exception


  }

 }
 void UpdateOrderDelivering(BO.Order order);
 OrderTracking OrderTrack(int orderID);
 BO.Order UpdateOrder(BO.Order order);

}
