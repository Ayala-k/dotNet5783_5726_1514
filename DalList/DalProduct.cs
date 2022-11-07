
using DO;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    public int addProduct(Product p)
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

    public void deleteProduct(int productID)
    {
        for (int i = 0; i < Config._productsEmptyIndex; i++)
            if (productID == _productsArr[i].ID)
            {
                _productsArr[i] = _productsArr[Config._productsEmptyIndex - 1];
                Config._productsEmptyIndex--;
            }
    }

    public void updateProduct(Product product)
    {
        for (int i = 0; i < Config._productsEmptyIndex; i++)
            if (product.ID == _productsArr[i].ID)
                _productsArr[i] = product;
    }

    public Product getProduct(int productID)
    {
        for (int i = 0; i < Config._productsEmptyIndex; i++)
            if (productID == _productsArr[i].ID)
                return _productsArr[i];
        throw new Exception("product does not exist");
    }

    public Product[] getAllProduct()
    {
        Product[] _productsCopy = new Product[Config._productsEmptyIndex - 1];
        for (int i = 0; i < Config._productsEmptyIndex - 1; i++)
            _productsCopy[i] = _productsArr[i];
        return _productsCopy;
    }
}
