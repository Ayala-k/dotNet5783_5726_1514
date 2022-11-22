
using BlTest.BO;

namespace BlTest.BlApi;

public interface ICart
{
    void AddOrderItem(OrderItem orderItem);
    void UpdateOrderItemAmountInStock(OrderItem orderItem);
    void CommitOrder();
}
