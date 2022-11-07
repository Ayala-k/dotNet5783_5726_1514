
using DO;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
 public void addProduct(Product p)
 {
  for (int i = 0; i != Config._productsEmptyIndex; i++)
  {
   if (p.ID == _productsArr[i].ID)
    return;
  }
  _productsArr[Config._productsEmptyIndex] = p;
  Config._productsEmptyIndex++;
 }

 public void deleteProduct(int productID)
 {
  for (int i = 0; i < Config._productsEmptyIndex; i++)
   if (productID == _productsArr[i].ID)
   {
    _productsArr[i] = _productsArr[i + 1];
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
  return null;
 }
}
//חסרה מתודה של קריאת כל האובייקטים של הישות