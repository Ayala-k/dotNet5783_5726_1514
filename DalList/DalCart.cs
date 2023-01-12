using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

internal class DalCart : ICart
{
 public int Add(Cart oi)//AddOrderItemToCart
 {
  return 0;
 }
 public void AddOrderItemToCart(OrderItem oi)//AddOrderItemToCart
 {
  userCart.ItemsList.Add(oi);
 }


 public void Delete(int productID)//AddOrderItemFronCart
 {
  userCart.ItemsList.Remove((userCart.ItemsList.FirstOrDefault(item => item?.ID == productID)));
 }

 public Cart getCart()
 {
  return userCart;
 }


 public IEnumerable<Cart?> GetAll(Func<Cart?, bool>? predict = null)
 {
  throw new NotImplementedException();
 }

 public Cart GetByCondition(Func<Cart?, bool> predict)
 {
  throw new NotImplementedException();
 }

 public void Update(Cart cart)//
 {
  userCart = cart;
 }

}
