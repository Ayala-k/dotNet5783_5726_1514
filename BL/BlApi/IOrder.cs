
namespace BL.BlApi;

public interface IOrder
{
 public IEnumerable<BO.OrderForList?> GetOrders();
 public BO.Order GetOrderDetails(int orderID);
 public BO.OrderForList GetOrderForListDetails(int orderID);

 public BO.Order UpdateOrderShipping(int orderID);
 public BO.Order UpdateOrderDelivering(int orderID);
 public BO.OrderTracking OrderTrack(int orderID);
 public void UpdateOrder(int orderID, int productID, int newAmount);
}
