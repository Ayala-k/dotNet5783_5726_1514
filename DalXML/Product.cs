using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class Product : IProduct
{
 string productPath = @"XMLProduct.xml";

 public int Add(DO.Product product)
 {
  XElement ProductRoot = XMLTools.LoadListFromXMLElement(productPath);

  var productIDsList = from p in ProductRoot.Elements()
                       select Convert.ToInt32(p.Element("ID")!.Value);
  if (productIDsList.FirstOrDefault(item => item == product.ID) != default(int))
   throw new DO.EntityAlreadyExistsException("product to add already exist");
  XElement productElement = new XElement("Product",
                new XElement("ID", product.ID),
                new XElement("Name", product.Name),
                new XElement("Category", product.Category),
                new XElement("Price", product.Price),
                new XElement("InStock", product.InStock));
  ProductRoot.Add(productElement);
  XMLTools.SaveListToXMLElement(ProductRoot, productPath);
  return product.ID;
 }

 /// <summary>
 /// Deletes an order from the file of orders
 /// </summary>
 /// <param name="id"></param>
 /// <exception cref="NotImplementedException"></exception>
 public void Delete(int productID)
 {
  XElement ProductRoot = XMLTools.LoadListFromXMLElement(productPath);
  try
  {
   (from product in ProductRoot.Elements()
    where (int)product.Element("ID")! == productID
    select product).FirstOrDefault()?.Remove();
  }
  catch
  {
   throw new EntityNotFoundException("product not found");
  }
  XMLTools.SaveListToXMLElement(ProductRoot, productPath);
 }

 /// <summary>
 /// Update an order in the file of orders
 /// </summary>
 /// <param name="t"></param>
 /// <exception cref="NotImplementedException"></exception>
 public void Update(DO.Product product)
 {
  XElement productElement = new XElement("Product",
              new XElement("ID", product.ID),
              new XElement("Name", product.Name),
              new XElement("Category", product.Category),
              new XElement("Price", product.Price),
              new XElement("InStock", product.InStock));
  XElement ProductRoot = XMLTools.LoadListFromXMLElement(productPath);
  try
  {
   (from p in ProductRoot.Elements()
    where (int)p.Element("ID")! == product.ID
    select p).FirstOrDefault()?.Remove();
  }
  catch
  {
   throw new EntityNotFoundException("product not found");
  }
  ProductRoot.Add(productElement);
  XMLTools.SaveListToXMLElement(ProductRoot, productPath);
 }

 /// <summary>
 /// Returns list of all the orders, if gets a condition - by it
 /// </summary>
 /// <param name="func"></param>
 /// <returns>IEnumerable<DO.Order?></returns>
 public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? predicate = null)
 {
  XElement ProductRoot = XMLTools.LoadListFromXMLElement(productPath);

  var productsList = from product in ProductRoot.Elements()
                     select new DO.Product()
                     {
                      ID = Convert.ToInt32(product.Element("ID")!.Value),
                      Name = product.Element("Name")!.Value,
                      Category = (DO.Categories)Enum.Parse(typeof(DO.Categories), product.Element("Category")!.Value),
                      Price = Convert.ToDouble(product.Element("Price")!.Value),
                      InStock = Convert.ToInt32(product.Element("InStock")!.Value)
                     };
  return predicate != null ? productsList.Cast<DO.Product?>().Where(predicate) : productsList.Cast<DO.Product?>();
 }

 /// <summary>
 /// Gets a condition and returns an order with this condition
 /// </summary>
 /// <param name="func"></param>
 /// <returns>DO.Order</returns>
 /// <exception cref="NotFound"></exception>
 public DO.Product GetByCondition(Func<DO.Product?, bool>? predicate)
 {
  XElement ProductRoot = XMLTools.LoadListFromXMLElement(productPath);

  IEnumerable<DO.Product> productsList = from product in ProductRoot.Elements()
                                         select new DO.Product()
                                         {
                                          ID = Convert.ToInt32(product.Element("ID")!.Value),
                                          Name = product.Element("Name")!.Value,
                                          Category = (DO.Categories)Enum.Parse(typeof(DO.Categories), product.Element("Category")!.Value),
                                          Price = Convert.ToDouble(product.Element("Price")!.Value),
                                          InStock = Convert.ToInt32(product.Element("InStock")!.Value)
                                         };
  return productsList.Cast<DO.Product?>().FirstOrDefault(predicate) ?? throw new EntityNotFoundException("product not found");
 }
}
