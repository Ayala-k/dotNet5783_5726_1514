
using BL.BO;

namespace BL.BlApi;

public interface ICart
{
    //art BlApi.ICart.AddOrderItem(BO.Cart cart,int productID)
    BO.Cart AddOrderItem(BO.Cart cart, int productID);
    BO.Cart UpdateOrderItemAmountInStock(BO.Cart cart, int productID, int updatedAmount);
    void CommitOrder();
}
