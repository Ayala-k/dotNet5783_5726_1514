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
    /// <exception cref="System.Exception"></exception>
    public int Add(Product p)
    {
        foreach (var item in _productsList)
        {
            if (item.ID == p.ID)
                throw new EntityAlreadyExistsException("product already exist");
        }
        _productsList.Add(p);
        return p.ID;
    }

    /// <summary>
    /// deleting product
    /// </summary>
    /// <param name="productID">id of product to delete</param>
    public void Delete(int productID)
    {
        foreach (var item in _productsList)
            if (item.ID == productID)
            {
                _productsList.Remove(item);
                return;
            }
        throw new EntityNotFoundException("product not found");
    }

    /// <summary>
    /// updating product
    /// </summary>
    /// <param name="product">product to update (by id)</param>
    public void Update(Product product)
    {
        for (var i = 0; i < _productsList.Count; i++)
        {
            if (_productsList[i].ID == product.ID)
            {
                _productsList[i] = product;
                return;
            }
        }
        throw new EntityNotFoundException("product not found");
    }

    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="productID">id of the requested product</param>
    /// <returns>requested product</returns>
    /// <exception cref="System.Exception"></exception>
    public Product Get(int productID)
    {
        foreach (var item in _productsList)
            if (item.ID == productID)
            {
               return item;
            }
        throw new EntityNotFoundException("product not found");
    }

    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of all products</returns>
    public IEnumerable<Product> GetAll()
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
        Product x = DataSource._productsList.First();
    }

}

