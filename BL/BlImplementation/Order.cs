
using BL.BlApi;
using Dal;
using DalApi;
using static BL.BO.OrderTracking;


namespace BL.BlImplementation;

internal class Order : BlApi.IOrder
{
    IDal Dal = new DalList();
    IEnumerable<BO.OrderForList> BlApi.IOrder.GetOrders()
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

    BO.Order BlApi.IOrder.GetOrderDetails(int orderID)
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
    BO.Order BlApi.IOrder.UpdateOrderShipping(int orderID)
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
    BO.Order BlApi.IOrder.UpdateOrderDelivering(int orderID)
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
    BO.OrderTracking BlApi.IOrder.OrderTrack(int orderID)
    {
        DO.Order orderDal=new DO.Order();
        try
        {
            orderDal = Dal.Order.Get(orderID);
        }
        catch
        {
            /////////////////////////////////////
        }
        List<DateAndProgressDescription> dateAndProgressDescriptionsList = new List<DateAndProgressDescription>();
        DateAndProgressDescription dateAndProgressDescription = new DateAndProgressDescription();
        OrderStatus status = findStatus(orderDal);
        if(status==OrderStatus.OrderCommited||status==OrderStatus.OrderShipped
            ||status==OrderStatus.OrderDelivered)
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
    //BO.Order UpdateOrder(BO.Order order) { } bonus
     
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
