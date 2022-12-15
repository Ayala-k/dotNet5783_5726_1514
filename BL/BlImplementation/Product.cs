
//using BO;
using BlImplementation;
using DalApi;
using System;
using System.Data;
using System.Reflection;

namespace BL.BlImplementation;

internal class Product : BlApi.IProduct
{
    IDal? Dal = Factory.Get();

    /// <summary>
    /// get all products or by condition
    /// </summary>
    /// <param name="predict">condition</param>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GetProducts(Func<DO.Product, bool>? predict = null)
 {
  IEnumerable<DO.Product?> productsListDal = Dal.Product.GetAll();
  List<BO.ProductForList> productsListBL = new List<BO.ProductForList>();
  foreach (DO.Product productDal in productsListDal)
  {
   if (predict == null || predict(productDal))
   {
    BO.ProductForList product = new BO.ProductForList();
    product = productDal!.copy(product);
    product.Category = (BO.Categories)productDal.Category;
    productsListBL.Add(product);
   }
  }
  return productsListBL;
 }

 /// <summary>
 /// get details abput specific product 
 /// </summary>
 /// <param name="productID"></param>
 /// <returns></returns>
 /// <exception cref="BO.EntityNotFoundLogicException"></exception>
 /// <exception cref="BO.InvalidDetailsException"></exception>
 public BO.Product GetProductDetailsManager(int productID)
 {
  if (productID > 0)
  {
   //get the product
   DO.Product productDal = new DO.Product();
   try
   {
    productDal = Dal.Product.GetByCondition(product => product.ID == productID);

   }
   catch (DO.EntityNotFoundException e)
   {
    throw new BO.EntityNotFoundLogicException("product not found", e);
   }
   BO.Product productBL = new BO.Product()
   {
    ID = productDal.ID,
    Name = productDal.Name,
    Price = productDal.Price,
    Category = (BO.Categories)productDal.Category,
    InStock = productDal.InStock
   };
   return productBL;
  }
  else
   throw new BO.InvalidDetailsException("invalid ID");
 }

 /// <summary>
 /// get details obout specific product
 /// </summary>
 /// <param name="productID"></param>
 /// <param name="cart"></param>
 /// <returns></returns>
 /// <exception cref="BO.EntityNotFoundLogicException"></exception>
 /// <exception cref="BO.NotEnoughInStockException"></exception>
 /// <exception cref="BO.InvalidDetailsException"></exception>
 public BO.ProductItem GetProductDetailsCustomer(int productID, BO.Cart cart)
 {
  if (productID > 0)
  {
   DO.Product productDal = new DO.Product();
   try
   {
    productDal = Dal.Product.GetByCondition(product => product.ID == productID);
   }
   catch (DO.EntityNotFoundException e)
   {
    throw new BO.EntityNotFoundLogicException("product not found", e);
   }

   int amountInCart = 0;
   foreach (BO.OrderItem oi in cart.ItemsList)
   {
    if (oi.ProductID == productID)
    {
     amountInCart = oi.Amount;
     break;
    }
   }
   if (amountInCart == 0)
    throw new BO.NotEnoughInStockException("not enaugh in stock");

   BO.ProductItem product = new BO.ProductItem()
   {
    ID = productDal.ID,
    Name = productDal.Name,
    Price = productDal.Price,
    Category = (BO.Categories)productDal.Category,
    AmountInCart = amountInCart,
    InStock = (amountInCart <= productDal.InStock)
   };
   return product;
  }
  else
   throw new BO.InvalidDetailsException("invalid ID");
 }

 /// <summary>
 /// add new product
 /// </summary>
 /// <param name="productBL"></param>
 /// <exception cref="BO.InvalidDetailsException"></exception>
 /// <exception cref="BO.EntityAlreadyExistsLogicException"></exception>
 public void AddProduct(BO.Product productBL)
 {
  //verify details
  if (productBL.ID <= 0 || productBL.Name == "" || productBL.Price <= 0 || productBL.InStock < 0)
   throw new BO.InvalidDetailsException("product details are invalid");

  DO.Product productDal = new DO.Product();
  productDal = productBL!.copy(productDal);
  productDal.Category = (DO.Categories)productBL.Category;
  //add
  try
  {
   Dal.Product.Add(productDal);
  }
  catch (DO.EntityAlreadyExistsException e)
  {
   throw new BO.EntityAlreadyExistsLogicException("product already exist", e);
  }
 }
 /// <summary>
 /// delete product
 /// </summary>
 /// <param name="productID"></param>
 /// <exception cref="BO.EntityInUseException"></exception>
 /// <exception cref="BO.EntityNotFoundLogicException"></exception>

 public void DeleteProduct(int productID)
 {
  //check that the product is not ordered by a customer now
  IEnumerable<DO.OrderItem?> orderItemsList = Dal.OrderItem.GetAll();
  foreach (DO.OrderItem oi in orderItemsList)
   if (oi.ProductID == productID &&
       Dal.Order.GetByCondition(o => o.ID == oi.OrderID).ShipDate == null)
    throw new BO.EntityInUseException("product exist in some orders, cannot be deleted");

  try
  {
   Dal.Product.Delete(productID);
  }
  catch (DO.EntityNotFoundException e)
  {
   throw new BO.EntityNotFoundLogicException("product not found", e);
  }
 }

 /// <summary>
 /// update product
 /// </summary>
 /// <param name="productBL"></param>
 /// <exception cref="BO.InvalidDetailsException"></exception>
 /// <exception cref="BO.EntityNotFoundLogicException"></exception>
 public void UpdateProduct(BO.Product productBL)
 {
  //check the details
  if (productBL.ID <= 0 || productBL.Name == "" || productBL.Price <= 0 || productBL.InStock < 0)
   throw new BO.InvalidDetailsException("product details are invalid");

  DO.Product productDal = new DO.Product()
  {
   ID = productBL.ID,
   Name = productBL.Name,
   Price = productBL.Price,
   Category = (DO.Categories)productBL.Category,
   InStock = productBL.InStock,
  };

  try
  {
   Dal.Product.Update(productDal);
  }
  catch (DO.EntityNotFoundException e)
  {
   throw new BO.EntityNotFoundLogicException("product not found", e);
  }
 }
}