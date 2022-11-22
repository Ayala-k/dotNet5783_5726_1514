
using BL.BO;

namespace BL.BlApi;

public interface IProduct
{
    IEnumerable<ProductForList> GetProducts();
    Product GetProductDetailsManager(int productID);
    ProductItem GetProductDetailsCustomer(int productID, Cart cart);
    void AddProduct(Product product);
    void DeleteProduct(int productID);
    void UpdateProduct(Product product);
}
