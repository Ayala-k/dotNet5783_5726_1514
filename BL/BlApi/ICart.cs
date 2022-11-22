
using BL.BO;

namespace BL.BlApi;

public interface ICart
{
    void AddOrderItem(OrderItem orderItem);
    void UpdateOrderItemAmountInStock(OrderItem orderItem);
    void CommitOrder();
}
