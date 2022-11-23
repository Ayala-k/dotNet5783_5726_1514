
using BL.BO;
using Dal;
using DalApi;
//using DO;

namespace BL.BlImplementation;

internal class Product : BlApi.IProduct
{
    IDal Dal = new DalList();
    IEnumerable<ProductForList> BlApi.IProduct.GetProducts()
    {
        IEnumerable<DO.Product> productsListDal = Dal.Product.GetAll();
        List<ProductForList> productsListBL = new List<ProductForList>();
        foreach (DO.Product productDal in productsListDal)
        {
            ProductForList product = new ProductForList()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (Categories)productDal.Category
            };
            productsListBL.Add(product);
        }
        return productsListBL;
    }
    BO.Product BlApi.IProduct.GetProductDetailsManager(int productID)
    {
        if (productID > 0)
        {
            DO.Product productDal = new DO.Product();
            try
            {
                productDal = Dal.Product.Get(productID);
            }
            catch (DO.EntityNotFoundException e)
            {
                //throw new Exception(e);
            }
            BO.Product productBL = new BO.Product()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (Categories)productDal.Category,
                InStock = productDal.InStock
            };
            return productBL;
        }
        else
            throw new Exception();
    }
    BO.ProductItem BlApi.IProduct.GetProductDetailsCustomer(int productID, Cart cart)
    {
        if (productID > 0)
        {
            DO.Product productDal = new DO.Product();
            try
            {
                productDal = Dal.Product.Get(productID);
            }
            catch (DO.EntityNotFoundException e)
            {
                //throw new Exception(e);
            }

            int amountInCart = 0;
            foreach (BO.OrderItem oi in cart.ItemsList)
            {
                if (oi.ID == productID)
                {
                    amountInCart = oi.Amount;
                    break;
                }
            }
            if (amountInCart == 0)
                throw new Exception();

            ProductItem product = new ProductItem()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (Categories)productDal.Category,
                AmountInCart = amountInCart,
                InStock = (amountInCart <= productDal.InStock)
            };
            return product;
        }
        else
            throw new Exception();
    }
    void BlApi.IProduct.AddProduct(BO.Product productBL)
    {
        if (productBL.ID <= 0 || productBL.Name == "" || productBL.Price <= 0 || productBL.InStock < 0)
            throw new Exception();
        //check what dan meant in the explanation---
        DO.Product productDal = new DO.Product()
        {
            ID = productBL.ID,
            Name = productBL.Name,
            Price = productBL.Price,
            Category = (DO.Categories)productBL.Category,
            InStock = productBL.InStock,
        };
        try
        {
            Dal.Product.Add(productDal);
        }
        catch (DO.EntityAlreadyExistsException e)
        {
            //throw new Exception();
        }
    }
    void BlApi.IProduct.DeleteProduct(int productID)
    {
        IEnumerable<BO.Order> ordersList = BO.Order.GetOrders();//fix
        foreach (BO.Order order in ordersList)
        {
            foreach (BO.OrderItem oi in order.ItemsList)
                if (oi.ID == productID)
                    throw new Exception();
        }
        try
        {
            Dal.Product.Delete(productID);
        }
        catch (DO.EntityNotFoundException e)
        {
            //throw new Exception();
        }
    }
    void BlApi.IProduct.UpdateProduct(BO.Product productBL)
    {
        if (productBL.ID <= 0 || productBL.Name == "" || productBL.Price <= 0 || productBL.InStock < 0)
            throw new Exception();

        DO.Product productDal = new DO.Product()
        {
            ID = productBL.ID,
            Name = productBL.Name,
            Price = productBL.Price,
            Category = (DO.Categories)productBL.Category,
            InStock = productBL.InStock,
        };

        try
        {
            Dal.Product.Update(productDal);
        }
        catch(DO.EntityNotFoundException e)
        {
            //throw new Exception();
        }
    }
}
