
using BL.BO;

namespace BL.BlApi;

public interface IProduct
{
    IEnumerable<Product> GetProducts();
    Product GetProductDetailsManager(int productID);
    Product GetProductDetailsCustomer(int productID);
    void AddProduct(Product product);
    void DeleteProduct(int productID);
    void UpdateProduct(Product product);
}
