
namespace BL.BlApi;

public interface ICart
{
 public BO.Cart AddOrderItem(BO.Cart cart, int productID);
 public BO.Cart UpdateOrderItemAmountInCart(BO.Cart cart, int productID, int updatedAmount);
 public int CommitOrder(BO.Cart cart);

 //login customer
 public void updateUserCart(BO.Cart cart);
 public BO.Cart getUserCart();
 public void addOrderItemUserCart(int productID);

}
