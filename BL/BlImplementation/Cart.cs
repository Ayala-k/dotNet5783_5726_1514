
using BL.BlApi;
using Dal;
using DalApi;

namespace BL.BlImplementation;

internal class Cart : ICart
{
 IDal Dal = new DalList();
 public BO.Cart AddOrderItem(BO.Cart cart, int productID)
 {
  bool productInCartBool = false;
  BO.OrderItem productInCart = new BO.OrderItem();
  foreach (var orderItem in cart.ItemsList)
  {
   if (orderItem.ID == productID)
   {
    productInCartBool = true;
    productInCart = orderItem;
    break;
   }
  }

  DO.Product productToAddToCart = new DO.Product();
  try
  {
   productToAddToCart = Dal.Product.Get(productID);
  }
  catch
  {
   //
  }

  if (!productInCartBool)
  {

   if (productToAddToCart.InStock == 0)
    throw new Exception();

   BO.OrderItem oi = new BO.OrderItem()
   {
    //ID=0,
    ProductID = productToAddToCart.ID,
    Name = productToAddToCart.Name,
    Price = productToAddToCart.Price,
    Amount = 1,
    TotalPrice = productToAddToCart.Price
   };
   cart.ItemsList.Add(oi);
   cart.TotalPrice += productToAddToCart.Price;
  }
  else
  {
   if (productToAddToCart.InStock <= productInCart.Amount)
    throw new Exception();

   cart.ItemsList.Remove(productInCart);
   productInCart.Amount++;
   productInCart.Price += productToAddToCart.Price;
   cart.TotalPrice += productToAddToCart.Price;
   cart.ItemsList.Add(productInCart);
  }
  return cart;
 }
 public BO.Cart UpdateOrderItemAmountInStock(BO.Cart cart, int productID, int updatedAmount)
 {
  BO.OrderItem productInCart = new BO.OrderItem();
  foreach (var orderItem in cart.ItemsList)
  {
   if (orderItem.ID == productID)
   {
    //productInCartBool = true;
    productInCart = orderItem;
    break;
   }
  }
  if (productInCart.Amount > updatedAmount)
  {
   for (int i = 0; i < (updatedAmount - productInCart.Amount); i++)
   {
    //Cart c = new Cart();
    //c.AddOrderItem(cart, productID);
    AddOrderItem(cart, productID);
   }
  }
  if (productInCart.Amount < updatedAmount)
  {
   cart.ItemsList.Remove(productInCart);
   int oldAmount = productInCart.Amount;
   productInCart.Amount = updatedAmount;
   productInCart.Price -= productInCart.Price * (oldAmount - updatedAmount);
   cart.TotalPrice -= productInCart.Price * (oldAmount - updatedAmount);
   cart.ItemsList.Add(productInCart);
  }
  if (updatedAmount == 0)
  {
   cart.TotalPrice -= productInCart.TotalPrice;
   cart.ItemsList.Remove(productInCart);
  }
  return cart;

 }
 public void CommitOrder(BO.Cart cart)
 {
  DO.Product product;
  foreach (BO.OrderItem item in cart.ItemsList)
  {
   try
   {
    product = Dal.Product.Get(item.ID);
   }
   catch
   {
    throw new Exception();//item not exist ??
   }
   if (item.Price < 0 || item.Amount < 0)
    throw new Exception();
   if (item.Amount > product.InStock)
    throw new Exception();
   item.TotalPrice = item.Amount * item.Price;
  }
  if (cart.CustomerName == " " || cart.CustomerAddress == " ")
   throw new Exception();
  if (cart.CustomerEmail == " ")//||פורמט לא חוקי
   throw new Exception();
  DO.Order orderDal = new DO.Order()
  {
   CustomerName = cart.CustomerName,
   CustomerEmail = cart.CustomerEmail,
   CustomerAddress = cart.CustomerAddress,
   OrderDate = DateTime.Now,
   ShipDate = default(DateTime),//מאופס?
   DeliveryDate = default(DateTime),
  };
  int orderDalID;
  try
  {
   orderDalID = Dal.Order.Add(orderDal);
  }
  catch
  {
   throw new Exception();
  }
  foreach (BO.OrderItem item in cart.ItemsList)
  {
   DO.OrderItem orderItemDal = new DO.OrderItem()
   {
    ID = item.ID,
    ProductID = item.ProductID,
    OrderID = orderDalID,
    Price = item.Price,
    Amount = item.Amount,
   };
   try
   {
    Dal.OrderItem.Add(orderItemDal);
   }
   catch
   {
    throw new Exception();
   }
   ///updating the stock
   product = Dal.Product.Get(item.ID);
   //Dal.Product.Delete(item.ID);
   product.InStock -= item.Amount;
   Dal.Product.Update(product);
  }
 }
}
