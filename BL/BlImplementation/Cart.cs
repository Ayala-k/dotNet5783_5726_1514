
using BL.BlApi;
using BlImplementation;
using DalApi;

namespace BL.BlImplementation;

internal class Cart : BlApi.ICart
{
 IDal? Dal = Factory.Get();
 BO.Cart cart = new BO.Cart();

 /// <summary>
 /// add an order item
 /// </summary>
 /// <param name="cart">cart to be added a product</param>
 /// <param name="productID">which product to add</param>
 /// <returns>the updated curt</returns>
 /// <exception cref="BO.EntityNotFoundLogicException">if product not exist</exception>
 /// <exception cref="BO.NotEnoughInStockException"></exception>
 public BO.Cart AddOrderItem(BO.Cart cart, int productID)
 {
  bool productInCartBool = false;

  //check if product already in cart
  BO.OrderItem productInCart = new BO.OrderItem();

  productInCart = cart.ItemsList.FirstOrDefault(item => item.ProductID == productID);
  if (productInCart != default(BO.OrderItem))
   productInCartBool = true;

  //get product details
  DO.Product productToAddToCart = new DO.Product();
  try
  {
   productToAddToCart = Dal?.Product.GetByCondition(item => item?.ID == productID) ?? throw new BO.EntityNotFoundLogicException("product to add not found"); ;
  }
  catch (DO.EntityNotFoundException e)
  {
   throw new BO.EntityNotFoundLogicException("product to add not found", e);
  };

  //if product not in cart
  if (!productInCartBool)
  {

   if (productToAddToCart.InStock == 0)
    throw new BO.NotEnoughInStockException("product to add not in stock");

   //add it to cart
   BO.OrderItem oi = new BO.OrderItem()
   {
    ProductID = productToAddToCart!.ID,
    Name = productToAddToCart.Name,
    Price = productToAddToCart.Price,
    Amount = 1,
    TotalPrice = productToAddToCart.Price
   };
   cart.ItemsList.Add(oi);
   cart.TotalPrice += productToAddToCart.Price;
  }
  //if product already in cart
  else
  {
   if (productToAddToCart.InStock < productInCart.Amount + 1)
    throw new BO.NotEnoughInStockException("not enough in stock");

   //update cart
   cart.ItemsList.Remove(productInCart);
   productInCart.Amount++;
   productInCart.TotalPrice += productInCart.Price;
   cart.TotalPrice += productInCart.Price;
   cart.ItemsList.Add(productInCart);
  }
  return cart;
 }

 /// <summary>
 /// update amount in stock of order item
 /// </summary>
 /// <param name="cart"></param>
 /// <param name="productID"></param>
 /// <param name="updatedAmount"></param>
 /// <returns></returns>
 public BO.Cart UpdateOrderItemAmountInCart(BO.Cart cart, int productID, int updatedAmount)
 {
  BO.OrderItem productInCart = cart.ItemsList.FirstOrDefault(item => item.ProductID == productID);
  int numOfProductInCart = productInCart.Amount;
  //if updated amount is bigger than before
  if (productInCart.Amount < updatedAmount && updatedAmount != 0)
  {
   {
    for (int i = 0; i < (updatedAmount - numOfProductInCart); i++)
     try
     {
      AddOrderItem(cart, productID);
     }
     catch (BO.EntityNotFoundLogicException e)
     {
      throw new BO.EntityNotFoundLogicException("product to add not found");
     }
     catch (BO.NotEnoughInStockException e)
     {
      throw new BO.NotEnoughInStockException("not enough in stock");
     }
   }
  }
  //if updated amount is amaller than before- update the cart
  else if (productInCart.Amount > updatedAmount && updatedAmount != 0)
  {
   cart.ItemsList.Remove(productInCart);
   int oldAmount = productInCart.Amount;
   productInCart.Amount = updatedAmount;
   //productInCart.TotalPrice = productInCart.Price * productInCart.Amount;

   productInCart.TotalPrice -= productInCart.Price * (oldAmount - updatedAmount);
   cart.TotalPrice -= productInCart.TotalPrice * (oldAmount - updatedAmount);
   cart.ItemsList.Add(productInCart);
  }
  //if updated ampunt is 0- upadte the cart
  else if (updatedAmount == 0)
  {
   cart.TotalPrice -= productInCart.TotalPrice;
   cart.ItemsList.Remove(productInCart);
  }
  return cart;
 }

 /// <summary>
 /// commit an order
 /// </summary>
 /// <param name="cart">cart to order</param>
 /// <exception cref="BO.EntityNotFoundLogicException"></exception>
 /// <exception cref="BO.InvalidDetailsException"></exception>
 /// <exception cref="BO.NotEnoughInStockException"></exception>
 /// <exception cref="BO.EntityAlreadyExistsLogicException"></exception>
 public int CommitOrder(BO.Cart cart)
 {
  //verify products in cart details
  DO.Product product;
  foreach (BO.OrderItem item in cart.ItemsList)
  {
   try
   {
    product = Dal?.Product.GetByCondition(p => p?.ID == item?.ProductID) ?? throw new BO.EntityNotFoundLogicException("one of the products not found"); ;
   }
   catch (DO.EntityNotFoundException e)
   {
    throw new BO.EntityNotFoundLogicException("one of the products not found", e);
   }

   if (item?.Price < 0 || item?.Amount < 0)
    throw new BO.InvalidDetailsException("one of the item's details are invalid");

   if (item.Amount > product.InStock)
    throw new BO.NotEnoughInStockException("one of the products does not have enough in stockS");

   item.TotalPrice = item.Amount * item.Price;
  }

  //verify customer details
  if (cart.CustomerName == "" || cart.CustomerAddress == "" || cart.CustomerEmail == "")
   throw new BO.InvalidDetailsException("invalid customer details");

  //create an order
  DO.Order orderDal = new DO.Order()
  {
   CustomerName = cart.CustomerName,
   CustomerEmail = cart.CustomerEmail,
   CustomerAddress = cart.CustomerAddress,
   OrderDate = DateTime.Now,
   ShipDate = null,
   DeliveryDate = null,
  };

  //add order
  int orderDalID;
  try
  {
   orderDalID = Dal?.Order.Add(orderDal) ?? throw new BO.EntityAlreadyExistsLogicException("product already exist");

   ;
  }
  catch (DO.EntityAlreadyExistsException e)
  {
   throw new BO.EntityAlreadyExistsLogicException("product already exist", e);
  }

  //add order items
  foreach (BO.OrderItem item in cart.ItemsList)
  {
   DO.OrderItem orderItemDal = new DO.OrderItem()
   {
    ProductID = item.ProductID,
    OrderID = orderDalID,
    Price = item.Price,
    Amount = item.Amount,
   };

   try
   {
    Dal?.OrderItem.Add(orderItemDal);
   }
   catch (DO.EntityAlreadyExistsException e)
   {
    throw new BO.EntityAlreadyExistsLogicException("product already exist", e);
   }

   //update stock
   try
   {
    product = Dal.Product.GetByCondition(p => p?.ID == item.ProductID);
   }
   catch (DO.EntityNotFoundException e)
   {
    throw new BO.EntityNotFoundLogicException("product not found", e);
   }

   product.InStock -= item.Amount;
   try
   {
    Dal?.Product.Update(product);
   }
   catch (DO.EntityNotFoundException e)
   {
    throw new BO.EntityNotFoundLogicException("product not found", e);
   }
  }
  return orderDalID;
 }



 public void updateUserCart(BO.Cart cart)//just user details and total price
 {
  DO.Cart cartDal = new DO.Cart()
  {
   CustomerName = cart.CustomerName,
   CustomerAddress = cart.CustomerAddress,
   CustomerEmail = cart.CustomerEmail,
   TotalPrice = cart.TotalPrice,
  };
  //cartDal.copy(cart);
  Dal.Cart.Update(cartDal);
 }
 public BO.Cart getUserCart()
 {
  //{
  // CustomerName=
  //}
  DO.Cart cartDal = Dal.Cart.getCart();
  BO.Cart cart = new BO.Cart()
  {
   CustomerName = cartDal.CustomerName,
   CustomerAddress = cartDal.CustomerAddress,
   CustomerEmail = cartDal.CustomerEmail,
   TotalPrice = cartDal.TotalPrice,
   //ItemsList=(BO.OrderItem)cartDal.ItemsList
  };
  foreach(DO.OrderItem oiDal in cartDal.ItemsList)
  {
   BL.BO.OrderItem oiBL = new BL.BO.OrderItem()
   {
    //Name = oiDal.Name,
    Price = oiDal.Price,
    ProductID = oiDal.ProductID,
    Amount = oiDal.Amount,
    TotalPrice = oiDal.Price * oiDal.Price,
   };
   cart.ItemsList.Add(oiBL);
  }
  return cart;
 }

 public void addOrderItemUserCart(int productID)
 {
  DO.Product productToAddToCart = new DO.Product();
  try
  {
   productToAddToCart = Dal?.Product.GetByCondition(item => item?.ID == productID) ?? throw new BO.EntityNotFoundLogicException("product to add not found"); ;
  }
  catch (DO.EntityNotFoundException e)
  {
   throw new BO.EntityNotFoundLogicException("product to add not found", e);
  }
  DO.OrderItem oi = new DO.OrderItem()
  {
   ProductID = productToAddToCart!.ID,
   //Name = productToAddToCart.Name,
   Price = productToAddToCart.Price,
   Amount = 1,
   //TotalPrice = productToAddToCart.Price
  };
  Dal.Cart.AddOrderItemToCart(oi);
  BL.BO.Cart cart = getUserCart();
 }








}
