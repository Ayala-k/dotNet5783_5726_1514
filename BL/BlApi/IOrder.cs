
using BL.BO;
using DO;

namespace BL.BlApi;

public interface IOrder
{
    IEnumerable<BlTest.BO.Order> GetOrders();
    BlTest.BO.Order GetOrderDetails(int orderID);
    void UpdateOrderShipping(BlTest.BO.Order order);
    void UpdateOrderDelivering(BlTest.BO.Order order);
    OrderTracking OrderTrack(int orderID);
    BlTest.BO.Order UpdateOrder(BlTest.BO.Order order);

}
