using BL.BO;

namespace BL.BlApi;

public interface IProduct
{
 public IEnumerable<BO.ProductForList?> GetProducts(Func<ProductForList, bool>? predict = null);
 public IEnumerable<BO.ProductItem?> GetProductIItems(Func<ProductItem, bool>? predict = null);
 public BO.Product GetProductDetailsManager(int productID);
 public BO.ProductForList GetProductForList(int productID);
 public BO.ProductItem GetProductDetailsCustomer(int productID, BO.Cart cart);
 public void AddProduct(BO.Product product);
 public void DeleteProduct(int productID);
 public void UpdateProduct(BO.Product product);
}
