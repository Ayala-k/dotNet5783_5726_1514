namespace DO;

public struct Cart
{
 public Cart()
 {
 CustomerName = null;
 CustomerAddress= null;
  CustomerEmail= null;
  TotalPrice = 0;
 }

 public string? CustomerName { get; set; }
 public string? CustomerEmail { get; set; }
 public string? CustomerAddress { get; set; }
 public List<OrderItem> ItemsList { get; set; } = new List<OrderItem>();
 public double TotalPrice { get; set; }
 public IEnumerable<int> OrdersIdList { get; set; }=new List<int>();

 public override string ToString()
 {
  string itemsList = "";
  foreach (OrderItem item in ItemsList)
  {
   itemsList += (item.ToString());
  }
  return ($@"
          Customer name: {CustomerName},
          Customer Email: {CustomerEmail},
          Customer Address: {CustomerAddress},
          Total Price: {TotalPrice},
          Items List: {itemsList}");
 }
}
