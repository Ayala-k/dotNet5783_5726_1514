using DO;

namespace DalApi;

public interface ICart : ICrud<Cart>
{
 public void AddOrderItemToCart(OrderItem oi);
 public Cart getCart();
}
