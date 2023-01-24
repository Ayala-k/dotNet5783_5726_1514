
namespace BL.BO;

public class OrderItem
{
 public OrderItem(){ }
 public OrderItem(OrderItem oi)
 {
  Name = oi.Name;
  ProductID = oi.ProductID;
  Price = oi.Price;
  Amount = oi.Amount;
  TotalPrice = oi.TotalPrice;
 }
 public string? Name { get; set; }
 public int ProductID { get; set; }
 public double Price { get; set; }
 public int Amount { get; set; }
 public double TotalPrice { get; set; }

 public override string ToString() => $@"
        OrderItem
        name: {Name},
        product ID: {ProductID},
        price: {Price},
        amount: {Amount},
        total price: {TotalPrice}";

}
