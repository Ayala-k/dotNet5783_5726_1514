
using BL;
namespace BL.BlApi;

public interface ICart
{
 public BO.Cart AddOrderItem(BO.Cart cart, int productID);
 public BO.Cart UpdateOrderItemAmountInStock(BO.Cart cart, int productID, int updatedAmount);
 public void CommitOrder(BO.Cart cart);
}
