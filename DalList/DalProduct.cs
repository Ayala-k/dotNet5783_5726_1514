using DO;
using static Dal.DataSource;
using DalApi;


namespace Dal;

/// <summary>
/// Accesing Products
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// adding product
    /// </summary>
    /// <param name="p">product to add</param>
    /// <returns>id of the added product</returns>
    /// <exception cref="System.Exception"></exception>
    public int Add(Product p)
    {
        if (_productsList.Exists(item => item?.ID == p.ID))
            throw new EntityAlreadyExistsException("product already exist");
        _productsList.Add(p);
        return p.ID;
    }

    /// <summary>
    /// deleting product
    /// </summary>
    /// <param name="productID">id of product to delete</param>
    public void Delete(int productID)
    {
        _productsList.Remove((_productsList.FirstOrDefault(item => item?.ID == productID))
            ?? throw new EntityNotFoundException("product not found"));
    }

    /// <summary>
    /// updating product
    /// </summary>
    /// <param name="product">product to update (by id)</param>
    public void Update(Product product)
    {
        for (var i = 0; i < _productsList.Count; i++)
        {
            if (_productsList[i]?.ID == product.ID)
            {
                _productsList[i] = product;
                return;
            }
        }
        throw new EntityNotFoundException("product not found");
    }

    /// <summary>
    /// get all the products
    /// </summary>
    /// <returns>array of all products</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? predict = null)
    {
        IEnumerable<Product?> productListCopy = new List<Product?>();
        productListCopy = from Product? p in _productsList
                          where (predict == null || predict(p))
                          select p;
        return productListCopy;
    }

    /// <summary>
    /// get specific product by condition
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Product GetByCondition(Func<Product?, bool> predict)
    {
        return (_productsList.FirstOrDefault(item => predict(item)))
             ?? throw new EntityNotFoundException("product not found");
    }

    /// <summary>
    /// a function for executing DataSource constructor
    /// (called in the beginning of the main prorgram
    /// </summary>
    public void initializeDataSource()
    {
        Product? x = DataSource._productsList.First();
    }

}

