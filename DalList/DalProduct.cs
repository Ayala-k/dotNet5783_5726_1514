using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// Accesing Products
/// </summary>
internal class DalProduct:IProduct
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
        //for (int i = 0; i < Config._productsEmptyIndex; i++)
        //    if (productID == _productsArr[i].ID)
        //        return _productsArr[i];
        //throw new Exception("product does not exist");
        List
    }

    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of all products</returns>
    public IEnumerable<Product> GetAllProduct()//check copy!!!!!!
    {
        List<Product> productsListCopy = _productsList;
        return productsListCopy;
    }
    
    /// <summary>
    /// a function for executing DataSource constructor
    /// (called in the beginning of the main prorgram
    /// </summary>
    public void initializeDataSource()
    {
        Order[] x = DataSource._ordersArr;
    }

}


/*using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// Accesing Products
/// </summary>
internal class DalProduct:IProduct
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
    public IEnumerable<Product> GetAllProduct()//check copy!!!!!!
    {
        //Product[] _productsCopy = new Product[Config._productsEmptyIndex];
        //for (int i = 0; i < Config._productsEmptyIndex; i++)
        //    _productsCopy[i] = _productsArr[i];
        //return _productsCopy;
        
        List<Product> productsListCopy = _productsList;
        return productsListCopy;
    }
    
    /// <summary>
    /// a function for executing DataSource constructor
    /// (called in the beginning of the main prorgram
    /// </summary>
    public void initializeDataSource()
    {
        Order[] x = DataSource._ordersArr;
    }

}
*/
