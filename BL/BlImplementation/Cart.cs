
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
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("product to add not found", e);
        };

        if (!productInCartBool)
        {

            if (productToAddToCart.InStock == 0)
                throw new BO.NotEnoughInStockException("product to add not in stock");

            BO.OrderItem oi = new BO.OrderItem()
            {
                //ID
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
                throw new BO.NotEnoughInStockException("not enough in stock");

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
                productInCart = orderItem;
                break;
            }
        }

        if (productInCart.Amount > updatedAmount)
        {
            for (int i = 0; i < (updatedAmount - productInCart.Amount); i++)
            {
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
                product = Dal.Product.Get(item.ProductID);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("one of the products not found", e);
            }

            if (item.Price < 0 || item.Amount < 0)
                throw new BO.InvalidDetailsException("one of the item's details are invalid");

            if (item.Amount > product.InStock)
                throw new BO.NotEnoughInStockException("one of the products does not have enough in stockS");

            item.TotalPrice = item.Amount * item.Price;
        }
        if (cart.CustomerName == " " || cart.CustomerAddress == " "|| cart.CustomerEmail == " ")
            throw new BO.InvalidDetailsException("invalid customer details");

        DO.Order orderDal = new DO.Order()
        {
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CustomerAddress = cart.CustomerAddress,
            OrderDate = DateTime.Now,
            ShipDate = default(DateTime),
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
                //ID = item.ID,
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

            try
            {
                product = Dal.Product.Get(item.ProductID);
            }
            catch(DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("product not found", e);
            }

            product.InStock -= item.Amount;
            try
            {
                Dal.Product.Update(product);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("product not found", e);
            }
        }
    }
}
