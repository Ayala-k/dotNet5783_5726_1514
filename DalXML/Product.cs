using DalApi;
using DO;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Product : IProduct
{
    string rootName = "ProductsList";
    string productPath = @"XMLProduct.xml";

    public int Add(DO.Product product)
    {
        List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        if (productsList.FirstOrDefault(item => item.Value.ID == product.ID) != null)
            throw new DO.EntityAlreadyExistsException("product to add already exist");
        productsList.Add(product);
        XMLTools.SaveListToXMLSerializer(productsList, productPath);
        return product.ID;
    }

    /// <summary>
    /// Deletes an order from the file of orders
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Delete(int productID)
    {
        List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        productsList.Remove((productsList.FirstOrDefault(item => item?.ID == productID))
            ?? throw new EntityNotFoundException("product not found"));
        XMLTools.SaveListToXMLSerializer(productsList, productPath);
    }

    /// <summary>
    /// Update an order in the file of orders
    /// </summary>
    /// <param name="t"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(DO.Product product)
    {
        List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        productsList.Remove((productsList.FirstOrDefault(item => item?.ID == product.ID)));
        productsList.Add(product);
        XMLTools.SaveListToXMLSerializer(productsList, productPath);
    }

    /// <summary>
    /// Returns list of all the orders, if gets a condition - by it
    /// </summary>
    /// <param name="func"></param>
    /// <returns>IEnumerable<DO.Order?></returns>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? predicate = null)
    {
        List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        return from product in productsList
               where predicate == null || predicate(product)
               select product;
    }

    /// <summary>
    /// Gets a condition and returns an order with this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns>DO.Order</returns>
    /// <exception cref="NotFound"></exception>
    public DO.Product GetByCondition(Func<DO.Product?, bool>? predicate)
    {
        List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product?>(productPath);
        return (productsList.FirstOrDefault(item => predicate(item)))
                       ?? throw new EntityNotFoundException("product not found");
    }
}


