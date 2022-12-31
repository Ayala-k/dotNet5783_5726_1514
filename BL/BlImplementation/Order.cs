
using DalApi;

namespace BL.BlImplementation;

internal class Order : BlApi.IOrder
{
    IDal? Dal = Factory.Get();

    /// <summary>
    /// return all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Order?> ordersListDal = Dal.Order.GetAll();
        IEnumerable<BO.OrderForList> orderListBL = from DO.Order orderDal in ordersListDal
                                                   select new BO.OrderForList()
                                                   {
                                                       ID = orderDal.ID,
                                                       CustomerName = orderDal.CustomerName,
                                                       Status = findStatus(orderDal),
                                                       AmountOfItems = findAmountOfItems(orderDal.ID),
                                                       TotalPrice = findTotalPrice(orderDal.ID)
                                                   };
        return orderListBL;
    }

    /// <summary>
    /// get order details by order ID
    /// </summary>
    /// <param name="orderID">order to get details about</param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFoundLogicException"></exception>
    /// <exception cref="BO.InvalidDetailsException"></exception>
    public BO.Order GetOrderDetails(int orderID)
    {
        if (orderID > 0)
        {
            //get order from DAL
            DO.Order orderDal = new DO.Order();
            try
            {
                orderDal = Dal?.Order.GetByCondition(order => order?.ID == orderID) ?? throw new BO.EntityNotFoundLogicException("order not found");
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("order not found", e);
            }

            //cast order to logic object
            IEnumerable<DO.OrderItem?> OrderItemsDal = new List<DO.OrderItem?>();
            OrderItemsDal = Dal.OrderItem.GetAll(item => item?.OrderID == orderID);
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
            throw new BO.InvalidDetailsException("invalid ID");
    }

    /// <summary>
    /// update order have been shipped
    /// </summary>
    /// <param name="orderID">order to be updated</param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFoundLogicException"></exception>
    /// <exception cref="BO.ProgressAlreadyDoneException"></exception>
    public BO.Order UpdateOrderShipping(int orderID)
    {
        //get the order
        DO.Order orderDal = new DO.Order();
        try
        {
            orderDal = Dal?.Order.GetByCondition(order => order?.ID == orderID) ?? throw new BO.EntityNotFoundLogicException("order not found");
        }
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("order not found", e);
        }

        //if the order has not been shipped yet- update ship date to now
        if (orderDal.ShipDate == null)
        {
            orderDal.ShipDate = DateTime.Now;
            try
            {
                Dal.Order.Update(orderDal);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("order not found", e);
            }

            IEnumerable<DO.OrderItem?> OrderItemsDal = new List<DO.OrderItem?>();
            try
            {
                OrderItemsDal = Dal.OrderItem.GetAll(item => item?.OrderID == orderID);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("order not found", e);
            }

            //cast order to a logic object
            BO.Order orderBL = new BO.Order()
            {
                ID = orderDal.ID,
                CustomerName = orderDal.CustomerName,
                CustomerEmail = orderDal.CustomerEmail,
                CustomerAddress = orderDal.CustomerAddress,
                Status = findStatus(orderDal),
                PaymentDate = orderDal.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryDate = orderDal.DeliveryDate,
                ItemsList = getOrderItem(OrderItemsDal),
                TotalPrice = findTotalPrice(orderDal.ID)
            };
            return orderBL;
        }
        else
            throw new BO.ProgressAlreadyDoneException("order has been shipped");
    }

    /// <summary>
    /// update order has been deleivered
    /// </summary>
    /// <param name="orderID">order that has been deleiverd</param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFoundLogicException"></exception>
    /// <exception cref="BO.ProgressAlreadyDoneException"></exception>
    public BO.Order UpdateOrderDelivering(int orderID)
    {
        //get the order
        DO.Order orderDal = new DO.Order();
        try
        {
            orderDal = Dal?.Order.GetByCondition(order => order?.ID == orderID) ?? throw new BO.EntityNotFoundLogicException("order not found");
        }
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("order not found", e);
        }

        //if the order has just been shipped-update delivery date to now
        if (orderDal.DeliveryDate == null
            && orderDal.ShipDate < DateTime.Now)
        {
            orderDal.DeliveryDate = DateTime.Now;
            try
            {
                Dal.Order.Update(orderDal);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("order not found", e);
            }

            IEnumerable<DO.OrderItem?> OrderItemsDal = new List<DO.OrderItem?>();
            try
            {
                OrderItemsDal = Dal.OrderItem.GetAll(item => item?.OrderID == orderID);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("order not found", e);
            }

            //cast order to logic object
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
            throw new BO.ProgressAlreadyDoneException("order has been delivered");
    }

    /// <summary>
    /// track order
    /// </summary>
    /// <param name="orderID">order to be tracked</param>
    /// <returns></returns>
    /// <exception cref="BO.EntityNotFoundLogicException"></exception>
    public BO.OrderTracking OrderTrack(int orderID)
    {
        //get the order
        DO.Order orderDal = new DO.Order();
        try
        {
            orderDal = Dal?.Order.GetByCondition(order => order?.ID == orderID) ?? throw new BO.EntityNotFoundLogicException("order not found");
        }
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("order not found", e);
        }

        //check order status by dates
        List<BO.OrderTracking.DateAndProgressDescription?> dateAndProgressDescriptionsList = new List<BO.OrderTracking.DateAndProgressDescription?>();
        BO.OrderTracking.DateAndProgressDescription dateAndProgressDescription = new BO.OrderTracking.DateAndProgressDescription();

        BO.OrderStatus status = findStatus(orderDal);

        if (status == BO.OrderStatus.OrderCommited || status == BO.OrderStatus.OrderShipped
            || status == BO.OrderStatus.OrderDelivered)
        {
            dateAndProgressDescription.ProgressDate = orderDal.OrderDate;
            dateAndProgressDescription.ProgressDescription = "order commited";
            dateAndProgressDescriptionsList.Add(dateAndProgressDescription);
        }
        if (status == BO.OrderStatus.OrderShipped || status == BO.OrderStatus.OrderDelivered)
        {
            dateAndProgressDescription.ProgressDate = orderDal.ShipDate;
            dateAndProgressDescription.ProgressDescription = "order shipped";
            dateAndProgressDescriptionsList.Add(dateAndProgressDescription);
        }
        if (status == BO.OrderStatus.OrderDelivered)
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

    /// <summary>
    /// update amount in stock of one of the products of an order
    /// </summary>
    /// <param name="orderID">order to update</param>
    /// <param name="productID">product to update</param>
    /// <param name="newAmount">updated amount</param>
    /// <exception cref="BO.EntityNotFoundLogicException"></exception>
    /// <exception cref="BO.ProgressAlreadyDoneException"></exception>
    /// <exception cref="BO.NotEnoughInStockException"></exception>
    public void UpdateOrder(int orderID, int productID, int newAmount)
    {
        //get the order
        DO.Order orderDal = new DO.Order();
        try
        {
            orderDal = Dal?.Order.GetByCondition(order => order?.ID == orderID) ?? throw new BO.EntityNotFoundLogicException("order not found");
        }
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("order not found", e);
        }

        //get the product
        DO.Product productDal = new DO.Product();
        try
        {
            productDal = Dal.Product.GetByCondition(p => p?.ID == productID);
        }
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("product not found", e);
        }

        IEnumerable<DO.OrderItem?> orderItems = Dal.OrderItem.GetAll();

        List<DO.OrderItem?> orderItemsList = new List<DO.OrderItem?>(orderItems);
        for (var i = 0; i < orderItemsList.Count; i++)
        {
            //find the order item
            if (orderItemsList[i]?.OrderID == orderID && orderItemsList[i]?.ProductID == productID)
            {
                //check that the update is possible
                if (orderDal.ShipDate <= DateTime.Now)
                    throw new BO.ProgressAlreadyDoneException("order has been sent");
                if (newAmount > productDal.InStock + orderItemsList[i]?.Amount)
                    throw new BO.NotEnoughInStockException("not enough products in stock");

                //update the order item
                DO.OrderItem newItem = orderItemsList[i].Value;
                newItem.Amount = newAmount;
                try
                {
                    Dal.OrderItem.Update(newItem);
                }
                catch (DO.EntityNotFoundException e)
                {
                    throw new BO.EntityNotFoundLogicException("order item not found", e);
                }

                //update amount in stock
                int oldAmount = orderItemsList[i]?.Amount ?? 0;
                productDal.InStock = productDal.InStock + oldAmount - newAmount;
                try
                {
                    Dal.Product.Update(productDal);
                }
                catch (DO.EntityNotFoundException e)
                {
                    throw new BO.EntityNotFoundLogicException("product not found", e);
                }
            }
        }

    }

    /// <summary>
    /// find status of an order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus findStatus(DO.Order order)
    {
        BO.OrderStatus status = BO.OrderStatus.OrderCommited;
        if (order.ShipDate < DateTime.Now)
            status = BO.OrderStatus.OrderShipped;
        if (order.DeliveryDate < DateTime.Now)
            status = BO.OrderStatus.OrderDelivered;
        return status;
    }

    /// <summary>
    /// find amount of items of an order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    private int findAmountOfItems(int orderID)
    {
        IEnumerable<DO.OrderItem?> orderItems = Dal.OrderItem.GetAll();
        int amountOfItems = orderItems
                .Where(item => item.Value.OrderID == orderID)
                .Count();
        return amountOfItems;
    }

    /// <summary>
    /// find total price of an order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    private double findTotalPrice(int orderID)
    {
        IEnumerable<DO.OrderItem?> orderItems = Dal.OrderItem.GetAll();
        double totalPrice = orderItems
            .Where(item => item.Value.OrderID == orderID)
            .Sum(item => item.Value.Price * item.Value.Amount);
        return totalPrice;
    }

 /// <summary>
 /// cast DO order items list to BO 
 /// </summary>
 /// <param name="OrderItemsDal"></param>
 /// <returns></returns>
 /// <exception cref="BO.EntityNotFoundLogicException"></exception>
 private List<BO.OrderItem?> getOrderItem(IEnumerable<DO.OrderItem?> OrderItemsDal)
 {
  IEnumerable<BO.OrderItem?> OrderItemsBL = new List<BO.OrderItem?>();
  try
  {
   OrderItemsBL = from DO.OrderItem oi in OrderItemsDal
                  let productDal = Dal?.Product.GetByCondition(product => product?.ID == oi.ProductID) ?? throw new BO.DalIsNullException("Dal is NULL")
                  select new BO.OrderItem()
                  {
                   Name = productDal.Name,
                   ProductID = oi.ProductID,
                   Price = oi.Price,
                   Amount = oi.Amount,
                   TotalPrice = oi.Price * oi.Amount
                  };
  }
  catch (DO.EntityNotFoundException e)
  {
   throw new BO.EntityNotFoundLogicException("one of the products not found", e);
  }
  return OrderItemsBL.ToList();
 }
}
