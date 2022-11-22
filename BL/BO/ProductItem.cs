
namespace BL.BO;

public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
    public bool InStock { get; set; }
    public int AmountInCart { get; set; }

}
