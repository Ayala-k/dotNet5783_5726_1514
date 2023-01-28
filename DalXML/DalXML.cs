using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
    private DalXml() { }
  
    private static DalXml instance;
    public static DalXml Instance
    {
        get
        {
            if (instance == null)
                instance = new DalXml();
            return instance;
        }
    }

    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    public  IProduct Product { get; } = new Dal.Product();
    public ICart Cart { get; } = new Dal.User();

}




