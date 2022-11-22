
using BlTest.BO;

namespace BlTest.BlApi;

public interface IProduct
{
    IEnumerable<Product> GetProducts();
    Product GetProductDetailsManager(int productID);
    Product GetProductDetailsCustomer(int productID);
    void AddProduct(Product product);
    void DeleteProduct();
    void UpdateProduct(Product product);
}
