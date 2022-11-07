using DO;
using static Dal.DataSource;

namespace Dal;

/// <summary>
/// Accesing Products
/// </summary>
public class DalProduct
{
    /// <summary>
    /// adding product
    /// </summary>
    /// <param name="p">product to add</param>
    /// <returns>id of the added product</returns>
    /// <exception cref="Exception"></exception>
    public int AddProduct(Product p)
    {
        for (int i = 0; i != Config._productsEmptyIndex; i++)
        {
            if (p.ID == _productsArr[i].ID)
                throw new Exception("product already exist");
        }
        _productsArr[Config._productsEmptyIndex] = p;
        Config._productsEmptyIndex++;
        return p.ID;
    }

    /// <summary>
    /// deleting product
    /// </summary>
    /// <param name="productID">id of product to delete</param>
    public void DeleteProduct(int productID)
    {
        for (int i = 0; i < Config._productsEmptyIndex; i++)
            if (productID == _productsArr[i].ID)
            {
                _productsArr[i] = _productsArr[Config._productsEmptyIndex - 1];
                Config._productsEmptyIndex--;
                break;
            }
    }

    /// <summary>
    /// updating product
    /// </summary>
    /// <param name="product">product to update (by id)</param>
    public void UpdateProduct(Product product)
    {
        for (int i = 0; i < Config._productsEmptyIndex; i++)
            if (product.ID == _productsArr[i].ID)
                _productsArr[i] = product;
    }

    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="productID">id of the requested product</param>
    /// <returns>requested product</returns>
    /// <exception cref="Exception"></exception>
    public Product GetProduct(int productID)
    {
        for (int i = 0; i < Config._productsEmptyIndex; i++)
            if (productID == _productsArr[i].ID)
                return _productsArr[i];
        throw new Exception("product does not exist");
    }

    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of all products</returns>
    public Product[] GetAllProduct()
    {
        Product[] _productsCopy = new Product[Config._productsEmptyIndex - 1];
        for (int i = 0; i < Config._productsEmptyIndex - 1; i++)
            _productsCopy[i] = _productsArr[i];
        return _productsCopy;
    }

}
