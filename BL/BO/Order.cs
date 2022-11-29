
namespace BL.BO;

public class Order
{
 public int ID { get; set; }
 public string? CustomerName { get; set; }
 public string? CustomerEmail { get; set; }
 public string? CustomerAddress { get; set; }
 public OrderStatus? Status { get; set; }
 public DateTime? PaymentDate { get; set; }
 public DateTime? ShipDate { get; set; }
 public DateTime? DeliveryDate { get; set; }
 public List<OrderItem?>? ItemsList { get; set; }
 public double TotalPrice { get; set; }




 public override string ToString()
 {
  string itemsList = "";
  foreach (OrderItem item in ItemsList)
  {
   itemsList += (item.ToString());
  }
  return (
 $@"
        Order ID:{ID},
        customer name: {CustomerName},
        customer email: {CustomerEmail},
        customer address: {CustomerAddress},
        order status: {Status},
        payment Date:{PaymentDate},
        ship Date:{ShipDate},
        delivery Date:{DeliveryDate},
        Items List:{itemsList},
        total price: {TotalPrice}");



 }
}
