
using DalApi;

namespace Dal;

sealed internal class DalList : IDal
{
 private DalList() { }
 public static IDal Instance { get; } = new DalList();
 public IProduct Product { get; } = new DalProduct();
 public IOrder Order { get; } = new DalOrder();
 public IOrderItem OrderItem { get; } = new DalOrderItem();
 public ICart Cart { get; } = new DalCart();

}