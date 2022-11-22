
using Dal;
using DalApi;

namespace BL.BlImplementation;

internal class Product : BlApi.IProduct
{
    IDal Dal = new DalList();
    IEnumerable<BO.Product> BlApi.IProduct.GetProducts()
    {
        IEnumerable<DO.Product> productsListDal = Dal.Product.GetAll();
        List<BO.Product> productsListBL = new List<BO.Product>();
        foreach (DO.Product productDal in productsListDal)
        {
            BO.Product productBL = new BO.Product();
            productBL.ID = productDal.ID;
            productBL.Name = productDal.Name;
            productBL.Price = productDal.Price;
            productBL.Category = (Categories)productDal.Category;
            productBL.InStock = productDal.InStock;
            productsListBL.Add(productBL);
        }
        return productsListBL;
    }
    BO.Product BlApi.IProduct.GetProductDetailsManager(int productID)
    {
        DO.Product productDal = Dal.Product.Get(productID);
        BO.Product productBL = new BO.Product();
        productBL.ID = productDal.ID;
        productBL.Name = productDal.Name;
        productBL.Price = productDal.Price;
        productBL.Category = (Categories)productDal.Category;
        productBL.InStock = productDal.InStock;
        return productBL;
    }
    BO.Product BlApi.IProduct.GetProductDetailsCustomer(int productID)
    {
        DO.Product productDal = Dal.Product.Get(productID);
        BO.Product productBL = new BO.Product();
        productBL.ID = productDal.ID;
        productBL.Name = productDal.Name;
        productBL.Price = productDal.Price;
        productBL.Category = (Categories)productDal.Category;
        return productBL;
    }
    void BlApi.IProduct.AddProduct(BO.Product productBL)
    {
        //check what dan meant in the explanation---
        DO.Product productDal = new DO.Product();
        productDal.ID = productBL.ID;
        productDal.Name = productBL.Name;
        productDal.Price = productBL.Price;
        productDal.Category = (DO.Categories)productBL.Category;
        productDal.InStock = productBL.InStock;
        Dal.Product.Add(productDal);
    }
    void BlApi.IProduct.DeleteProduct(int productID)
    {
        Dal.Product.Delete(productID);
    }
    void BlApi.IProduct.UpdateProduct(BO.Product productBL)
    {
        DO.Product oldProduct=Dal.Product.Get(productBL.ID);
        if (!(productBL.Name==null))
            oldProduct.Name = productBL.Name;
        if (!(productBL.Price == null))
            oldProduct.Price = productBL.Price;
        if (!(productBL.Category == null))
            oldProduct.Category = (DO.Categories)productBL.Category;
        if (!(productBL.InStock == null))
            oldProduct.InStock = productBL.InStock;
        Dal.Product.Update(oldProduct);
    }
}
