using BL;
using BL.BlImplementation;
namespace BlTest
{
 internal class Program
 {
  private static readonly BL.BlApi.IBl bl = new BlImplementation.Bl();

  static void Main(string[] args)
  {
   Console.WriteLine("enter 1 to product, 2 to order, 3 to order item, 0 to exit");
   int choice;
   int.TryParse(Console.ReadLine(), out choice);

   while (choice != 0)
   {
    switch (choice)
    {
     case 1://product
      {
       Program.productMethod();
       break;
      }
     case 2://order
      {
       Program.orderMethod();
       break;
      }
     case 3://order item
      {
       Program.cartMethod();
       break;
      }
    }
    Console.WriteLine("enter 1 to product, 2 to order, 3 to order item, 0 to exit");
    int.TryParse(Console.ReadLine(), out choice);
   }
  }
  private static void productMethod()
  {
   BL.BO.Product product = new BL.BO.Product();
   BL.BO.Cart cart = new BL.BO.Cart();

   Console.WriteLine("enter 1 to view product's list," +
    " 2 to view product's details(for manager)," +
    " 3 to add a product,4 to delete a product," +
    "5 to update a product" +
    " 6 to view product's details(for costumer)");
   int choiceProduct;
   int.TryParse(Console.ReadLine(), out choiceProduct);
   int parse;
   double parseDouble;
   Categories parseCategory;
   switch (choiceProduct)
   {
    case 1://view all products
     IEnumerable<BL.BO.ProductForList> pList = bl.Product.GetProducts();
     foreach (BL.BO.ProductForList x in pList)
      Console.WriteLine(x);
     break;

    case 2://view product details for manager
     Console.WriteLine("enter product ID");
     int.TryParse(Console.ReadLine(), out parse);

     Console.WriteLine(bl.Product.GetProductDetailsManager(parse));
     break;

    case 3://add product
     Console.WriteLine("enter product details");
     int.TryParse(Console.ReadLine(), out parse);
     product.ID = parse;
     product.Name = Console.ReadLine();
     int.TryParse(Console.ReadLine(), out parse);
     product.Price = parse;
     Categories.TryParse(Console.ReadLine(), out parseCategory);
     product.Category = parseCategory;
     int.TryParse(Console.ReadLine(), out parse);
     product.InStock = parse;
     bl.Product.AddProduct(product);
     break;

    case 4://delete product
     Console.WriteLine("enter product ID");
     int.TryParse(Console.ReadLine(), out parse);
     bl.Product.DeleteProduct(parse);
     break;

    case 5://update product
     Console.WriteLine("enter product details");
     int.TryParse(Console.ReadLine(), out parse);
     product.ID = parse;
     product.Name = Console.ReadLine();
     int.TryParse(Console.ReadLine(), out parse);
     product.Price = parse;
     Categories.TryParse(Console.ReadLine(), out parseCategory);
     product.Category = parseCategory;
     int.TryParse(Console.ReadLine(), out parse);
     product.InStock = parse;
     bl.Product.UpdateProduct(product);
     break;

    case 6://view product details for customer
     Console.WriteLine("enter product ID ");
     int.TryParse(Console.ReadLine(), out parse);
     product.ID = parse;
     //cart.CustomerName = Console.ReadLine();
     //cart.CustomerEmail = Console.ReadLine();
     //cart.CustomerAddress = Console.ReadLine();

     //Console.WriteLine("enter product ID");
     //int productID;
     //int.TryParse(Console.ReadLine(), out parse);
     //productID = parse;
     //Console.WriteLine("enter amount of items");
     //int amount;
     //int.TryParse(Console.ReadLine(), out parse);
     //amount = parse;
     //cart.ItemsList = new List<BL.BO.OrderItem>();
     //while (amount > 0 && productID > 0)
     //{
     // BL.BO.OrderItem orderItem = new BL.BO.OrderItem()
     // {
     //  Name = bl.Product.GetProductDetailsManager(productID).Name,
     //  ProductID = productID,
     //  Price = bl.Product.GetProductDetailsManager(productID).Price,//manager
     //  Amount = amount
     // };
     // cart.ItemsList.Add(orderItem);

     // Console.WriteLine("enter product ID");
     // int.TryParse(Console.ReadLine(), out parse);
     // productID = parse;

     // Console.WriteLine("enter amount of items");
     // int.TryParse(Console.ReadLine(), out parse);
     // amount = parse;
     //}
     CartItemsMethod(cart);
     Console.WriteLine(bl.Product.GetProductDetailsCustomer(parse, cart));
     break;
   }

  }
  private static void orderMethod()
  {
   BL.BO.Order order = new BL.BO.Order();
   BL.BO.OrderTracking orderTracking = new BL.BO.OrderTracking();

   Console.WriteLine("enter 1 to view order's list," +
    " 2 to view order's details," +
    " 3 to update a order's date shipping" +
    ",4 to update a order's date delivery," +
    "5 to tracking the order" +
    " 6 to update order(bonus)");
   int choiceOrder;
   int.TryParse(Console.ReadLine(), out choiceOrder);
   int parse;
   double parseDouble;
   //Categories parseCategory;
   //BL.BO.OrderItem parseOrderItem;
   switch (choiceOrder)
   {
    case 1://view all orders
     IEnumerable<BL.BO.OrderForList> oList = bl.Order.GetOrders();
     foreach (BL.BO.OrderForList x in oList)
      Console.WriteLine(x);
     break;

    case 2://view order details for manager
     Console.WriteLine("enter order ID");
     int.TryParse(Console.ReadLine(), out parse);
     Console.WriteLine(bl.Order.GetOrderDetails(parse));
     break;

    case 3://update shipping date
     Console.WriteLine("enter order ID");
     int.TryParse(Console.ReadLine(), out parse);
     order = bl.Order.UpdateOrderShipping(parse);
     Console.WriteLine(order);
     break;

    case 4://update delivert date
     Console.WriteLine("enter order ID");
     int.TryParse(Console.ReadLine(), out parse);
     order = bl.Order.UpdateOrderDelivering(parse);
     Console.WriteLine(order);
     break;

    case 5://order tracking
     Console.WriteLine("enter order ID");
     int.TryParse(Console.ReadLine(), out parse);
     orderTracking = bl.Order.OrderTrack(parse);
     Console.WriteLine(orderTracking);
     break;


    case 6://update order
     Console.WriteLine("enter order ID,product id and new amount for updating");
     int orderId;
     int productId;
     int newAmount;
     int.TryParse(Console.ReadLine(), out orderId);
     int.TryParse(Console.ReadLine(), out productId);
     int.TryParse(Console.ReadLine(), out newAmount);
     try
     {
      bl.Order.UpdateOrder(orderId, productId, newAmount);
     }
     catch
     {
     }
      break;
   }
  }

  private static void cartMethod()
  {
   BL.BO.Cart cart = new BL.BO.Cart();
   BL.BO.Product product = new BL.BO.Product();
   Console.WriteLine("enter 1 to add product to the cart," +
    " 2 to update product amount in the cart," +
    " 3 to make the order");
   int choiceOrder;
   int.TryParse(Console.ReadLine(), out choiceOrder);
   int parse;
   switch (choiceOrder)
   {
    case 1://add product to the cart
     Console.WriteLine("enter product id ");
     int.TryParse(Console.ReadLine(), out parse);
     int productID = parse;
     //cart.CustomerName = Console.ReadLine();
     //cart.CustomerEmail = Console.ReadLine();
     //cart.CustomerAddress = Console.ReadLine();
     CartItemsMethod(cart);
     Console.WriteLine(bl.Cart.AddOrderItem(cart, productID));
     break;
    case 2:
     Console.WriteLine("enter product id ,new amount ");
     int.TryParse(Console.ReadLine(), out parse);
     productID = parse;
     int.TryParse(Console.ReadLine(), out parse);
     int newAmount = parse;
     //cart.CustomerName = Console.ReadLine();
     //cart.CustomerEmail = Console.ReadLine();
     //cart.CustomerAddress = Console.ReadLine();
     //Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");

     //int.TryParse(Console.ReadLine(), out parse);
     //productId = parse;
     //int.TryParse(Console.ReadLine(), out parse);
     //amount = parse;
     //cart.ItemsList = new List<BL.BO.OrderItem>();
     //while (productId != 0)
     //{
     // BL.BO.OrderItem orderItem = new BL.BO.OrderItem()
     // {
     //  Name = bl.Product.GetProductDetailsManager(productId).Name,
     //  ProductID = productId,
     //  Price = bl.Product.GetProductDetailsManager(productId).Price,//manager
     //  Amount = amount
     // };
     // cart.ItemsList.Add(orderItem);
     // Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
     // int.TryParse(Console.ReadLine(), out parse);
     // productId = parse;
     // int.TryParse(Console.ReadLine(), out parse);
     // amount = parse;
     //}
     CartItemsMethod(cart);
     Console.WriteLine(bl.Cart.UpdateOrderItemAmountInStock(cart, productID, newAmount));
     break;

    case 3:
     //Console.WriteLine("enter your details ");
     //cart.CustomerName = Console.ReadLine();
     //cart.CustomerEmail = Console.ReadLine();
     //cart.CustomerAddress = Console.ReadLine();
     //Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");

     //int.TryParse(Console.ReadLine(), out parse);
     //productId = parse;
     //int.TryParse(Console.ReadLine(), out parse);
     //amount = parse;
     //cart.ItemsList = new List<BL.BO.OrderItem>();
     //while (productId != 0)
     //{
     // BL.BO.OrderItem orderItem = new BL.BO.OrderItem()
     // {
     //  Name = bl.Product.GetProductDetailsManager(productId).Name,
     //  ProductID = productId,
     //  Price = bl.Product.GetProductDetailsManager(productId).Price,//manager
     //  Amount = amount
     // };
     // cart.ItemsList.Add(orderItem);
     // Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
     // int.TryParse(Console.ReadLine(), out parse);
     // productId = parse;
     // int.TryParse(Console.ReadLine(), out parse);
     // amount = parse;
     //}
     CartItemsMethod(cart);
     bl.Cart.CommitOrder(cart);
     break;
   }


  }
  private static BL.BO.Cart CartItemsMethod(BL.BO.Cart cart)
  {
   Console.WriteLine("enter your details ");
   cart.CustomerName = Console.ReadLine();
   cart.CustomerEmail = Console.ReadLine();
   cart.CustomerAddress = Console.ReadLine();
   Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
   int productId;
   int amount;
   int parse;
   int.TryParse(Console.ReadLine(), out parse);
   productId = parse;
   int.TryParse(Console.ReadLine(), out parse);
   amount = parse;
   cart.ItemsList = new List<BL.BO.OrderItem>();
   while (productId != 0)
   {
    BL.BO.OrderItem orderItem = new BL.BO.OrderItem()
    {
     Name = bl.Product.GetProductDetailsManager(productId).Name,//manager
     ProductID = productId,
     Price = bl.Product.GetProductDetailsManager(productId).Price,//manager
     Amount = amount
    };
    cart.ItemsList.Add(orderItem);

    Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
    int.TryParse(Console.ReadLine(), out parse);
    productId = parse;
    int.TryParse(Console.ReadLine(), out parse);
    amount = parse;
   }
   return cart;
  }
 }
}


