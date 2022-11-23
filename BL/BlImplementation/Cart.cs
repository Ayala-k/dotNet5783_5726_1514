
using BL.BlApi;
using Dal;
using DalApi;

namespace BL.BlImplementation;

internal class Cart: BlApi.ICart
{
    IDal Dal = new DalList();
    BO.Cart BlApi.ICart.AddOrderItem(BO.Cart cart,int productID)
    {
        bool productInCartBool = false;
        BO.OrderItem productInCart=new BO.OrderItem();
        foreach (var orderItem in cart.ItemsList)
        {
            if (orderItem.ID == productID)
            {
                productInCartBool = true;
                productInCart = orderItem;
                break;
            }
        }

        DO.Product productToAddToCart=new DO.Product();
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

            BO.OrderItem oi= new BO.OrderItem()
            {
                //ID=0,
                ProductID= productToAddToCart.ID,
                Name= productToAddToCart.Name,
                Price=productToAddToCart.Price,
                Amount=1,
                TotalPrice=productToAddToCart.Price
            };
            cart.ItemsList.Add(oi);
            cart.TotalPrice+=productToAddToCart.Price;
        }
        else
        {
            if(productToAddToCart.InStock<=productInCart.Amount)
                throw new Exception();

            cart.ItemsList.Remove(productInCart);
            productInCart.Amount++;
            productInCart.Price+=productToAddToCart.Price;
            cart.TotalPrice += productToAddToCart.Price;
            cart.ItemsList.Add(productInCart);
        }
        return cart;
    }
    BO.Cart BlApi.ICart.UpdateOrderItemAmountInStock(BO.Cart cart, int productID, int updatedAmount)
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
            productInCart.Amount=updatedAmount;
            productInCart.Price -= productInCart.Price*(oldAmount-updatedAmount);
            cart.TotalPrice-= productInCart.Price * (oldAmount - updatedAmount);
            cart.ItemsList.Add(productInCart);
        }
        if (updatedAmount == 0)
        {
            cart.TotalPrice -= productInCart.TotalPrice;
            cart.ItemsList.Remove(productInCart);
        }
        return cart;

    }
    void BlApi.ICart.CommitOrder() { }
    //(List<OrderItem> orderItemsList, int productID)
}
