
namespace BL.BO;


public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<OrderItem> ItemsList { get; set; }
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
        Customer name: {CustomerName},
        Customer Email: {CustomerEmail},
        Customer Address: {CustomerAddress},
        Items List: {itemsList}");
 }

}
