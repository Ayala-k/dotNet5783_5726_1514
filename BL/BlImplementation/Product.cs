
using BO;
using Dal;
using DalApi;

namespace BL.BlImplementation;

internal class Product : BlApi.IProduct
{
    IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        IEnumerable<DO.Product?> productsListDal = Dal.Product.GetAll();
        List<BO.ProductForList> productsListBL = new List<BO.ProductForList>();
        foreach (DO.Product productDal in productsListDal)
        {
            BO.ProductForList product = new BO.ProductForList()
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
    public BO.Product GetProductDetailsManager(int productID)
    {
        if (productID > 0)
        {
            DO.Product productDal = new DO.Product();
            try
            {
                productDal = Dal.Product.GetByCondition(product => product.ID == productID);

            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("product not found", e);
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
            throw new BO.InvalidDetailsException("invalid ID");
    }
    public BO.ProductItem GetProductDetailsCustomer(int productID, BO.Cart cart)
    {
        if (productID > 0)
        {
            DO.Product productDal = new DO.Product();
            try
            {
                productDal = Dal.Product.GetByCondition(product => product.ID == productID);
            }
            catch (DO.EntityNotFoundException e)
            {
                throw new BO.EntityNotFoundLogicException("product not found", e);
            }

            int amountInCart = 0;
            foreach (BO.OrderItem oi in cart.ItemsList)
            {
                if (oi.ProductID == productID)
                {
                    amountInCart = oi.Amount;
                    break;
                }
            }
            if (amountInCart == 0)
                throw new BO.NotEnoughInStockException("not enaugh in stock");

            BO.ProductItem product = new BO.ProductItem()
            {
                ID = productDal.ID,
                Name = productDal.Name,
                Price = productDal.Price,
                Category = (BO.Categories)productDal.Category,
                AmountInCart = amountInCart,
                InStock = (amountInCart <= productDal.InStock)
            };
            return product;
        }
        else
            throw new BO.InvalidDetailsException("invalid ID");
    }
    public void AddProduct(BO.Product productBL)
    {
        if (productBL.ID <= 0 || productBL.Name == "" || productBL.Price <= 0 || productBL.InStock < 0)
            throw new BO.InvalidDetailsException("product details are invalid");

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
            throw new BO.EntityAlreadyExistsLogicException("product already exist", e);
        }
    }
    public void DeleteProduct(int productID)
    {
        IEnumerable<DO.OrderItem?> orderItemsList = Dal.OrderItem.GetAll();
        foreach (DO.OrderItem oi in orderItemsList)
            if (oi.ID == productID)
                throw new BO.EntityInUseException("product exist in some orders, cannot be deleted");

        try
        {
            Dal.Product.Delete(productID);
        }
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("product not found", e);
        }
    }
    public void UpdateProduct(BO.Product productBL)
    {
        if (productBL.ID <= 0 || productBL.Name == "" || productBL.Price <= 0 || productBL.InStock < 0)
            throw new BO.InvalidDetailsException("product details are invalid");

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
        catch (DO.EntityNotFoundException e)
        {
            throw new BO.EntityNotFoundLogicException("product not found", e);
        }
    }
}