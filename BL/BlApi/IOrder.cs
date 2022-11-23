
using BL.BO;

namespace BL.BlApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetOrders();
    BO.Order GetOrderDetails(int orderID);
    BO.Order UpdateOrderShipping(int orderID);
    BO.Order UpdateOrderDelivering(int orderID);
    OrderTracking OrderTrack(int orderID);
    //BO.Order UpdateOrder(BO.Order order);
}
